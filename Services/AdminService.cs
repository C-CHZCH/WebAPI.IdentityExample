using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.IdentityExample.Contract.AdminContract;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Model;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Services;

public class AdminService : IAdminService
{
    private readonly AuthDbContext _authDbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SchoolDbContext _schoolDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        AuthDbContext authDbContext, SchoolDbContext schoolDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authDbContext = authDbContext;
        _schoolDbContext = schoolDbContext;
    }

    public async Task<List<UserResponse>> GetAllAccount()
    {
        var res = new List<UserResponse>();
        var userresult = await _userManager.Users.ToListAsync();
        foreach (var user in userresult)
        {
            var roles = await _userManager.GetRolesAsync(user);
            res.Add(new UserResponse
            {
                UserName = user.UserName,
                UserRoleName = roles.Aggregate((current, next) => current + ", " + next),
                UserClassId = user.ClassId,
                UserClassName = user.ClassName ?? string.Empty
            });
        }

        return res;
    }

    public async Task<Response> AddUserToTeacherRequest(AddUsertoTeacherRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
            return new Response
            {
                Status = "failure",
                Message = "Please input correct username"
            };

        var result = await _userManager.AddToRoleAsync(user, UserRole.Teacher);
        if (result.Succeeded)
            return new Response
            {
                Status = "Success",
                Message = "OK"
            };
        return new Response
        {
            Status = "failure",
            Message = "fail to add role"
        };
    }

    public async Task<List<AllClassProfileResponse>> GetAllClassProfile()
    {
        await using (_schoolDbContext)
        {
            var result = await _schoolDbContext.classes.ToListAsync();
            var ret = result.Select(r => new AllClassProfileResponse
                {
                    ClassName = r.Name,
                    ClassCode = r.Code,
                    ClassId = r.Id,
                    ClassNumber = r.Number,
                    isLockOut = r.IsLockout,
                    CreateTime = r.CreateOn,
                    UpdateTime = r.UpdateOn,
                    Description = r.Description
                })
                .ToList();
            return ret;
        }
    }

    public async Task<List<AllClassHomeworkResponse>> AllHomeworkProfile()
    {
        await using (_schoolDbContext)
        {
            var result = await _schoolDbContext.homeworks.ToListAsync();
            var ret = result.Select(r => new AllClassHomeworkResponse
            {
                Id = r.Id,
                CreateOn = r.CreateOn,
                LasTime = r.LasTime,
                Name = r.Name,
                Description = r.Description,
                Number = r.Number,
                Url = r.Url
            }).ToList();
            return ret;
        }
    }

    public async Task<Response> EditUserPassword(string username, string newpassword)
    {
        var result = await _userManager.FindByNameAsync(username);
        if (result == null)
            return new Response
            {
                Status = "failure",
                Message = "User Null"
            };
        await _userManager.RemovePasswordAsync(result);
        var ret = await _userManager.AddPasswordAsync(result, newpassword);
        if (ret.Succeeded)
            return new Response
            {
                Status = "Success",
                Message = "Ok"
            };
        return new Response
        {
            Status = "failure",
            Message = "failure"
        };
    }
}