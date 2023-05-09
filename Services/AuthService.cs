using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OneOf;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.Model;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Services;

/// <summary>
///     用于登录以及注册的服务
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJWTService _jwtService;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IJWTService jwtService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
        _signInManager = signInManager;
    }

    /// <summary>
    ///     登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<OneOf<AuthLoginResponse, AuthLoginFailureResponse>> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return new AuthLoginFailureResponse
            {
                Status = "LoginFailure",
                Message = "Please check Password or Username"
            };


        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(
            userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());

        var token = _jwtService.GetToken(authClaims);

        return new AuthLoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ValidTo = token.ValidTo,
        };
    }

    /// <summary>
    ///     注册
    /// </summary>
    /// <param name="model">注册是上传的基本信息：用户名，密码，邮箱等</param>
    /// <param name="role">需要注册的角色，由对应的控制器所选择</param>
    /// <returns></returns>
    public async Task<OneOf<RegisterSuccessResponse, RegisterFailedResponse>> Register(RegisterModel model, string role)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists is not null)
            return new UserAlreadyExistsResponse();

        var user = new ApplicationUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username,
            Id = Guid.NewGuid().ToString()
        };

        if (!await _roleManager.RoleExistsAsync(role))
            return new NonExistingRoleResponse { Role = role };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new RegisterFailedResponse { Message = "Unable to create user." };

        result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
            return new RegisterFailedResponse { Message = "Unable to add user to role." } ;

        return new RegisterSuccessResponse(user);
    }

    /// <summary>
    ///     获取指定账户名的角色信息
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<string> GetRole(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
            return "User Null";
        var role = await _roleManager.FindByNameAsync(username);
        return role!.Name!;
    }

    /// <summary>
    ///     注册基本角色
    /// </summary>
    /// <returns></returns>
    public async Task SeedRoles()
    {
        foreach (var role in UserRole.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    /*public static string GenerateRandomString(int length)
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var rng = new RNGCryptoServiceProvider();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        var result = new StringBuilder(length);
        foreach (var b in bytes)
        {
            result.Append(validChars[b % validChars.Length]);
        }
        return result.ToString();
    }*/
}
