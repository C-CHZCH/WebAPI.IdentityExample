using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using WebAPI.IdentityExample.Contract.AccountContract;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Model;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Services;

/// <summary>
///     与账号信息有关的服务
/// </summary>
public class AccountService : IAccountService
{
    public AccountService(UserManager<ApplicationUser> userManager, SchoolDbContext context,
        AuthDbContext authDbContext, RoleManager<IdentityRole> roleManager)
    {
        UserManager = userManager;
        Context = context;
        AuthDbContext = authDbContext;
        RoleManager = roleManager;
    }

    private UserManager<ApplicationUser> UserManager { get; }
    private SchoolDbContext Context { get; }

    private AuthDbContext AuthDbContext { get; }

    private RoleManager<IdentityRole> RoleManager { get; }

    /// <summary>
    ///     修改用户的密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<OneOf<Response, None>> EditPassword(EditPasswordRequest model)
    {
        if (model.username == null) return new None();
        var user = await UserManager.FindByNameAsync(model.username);
        if (user == null)
            return new Response
            {
                Status = "failure",
                Message = "User Null"
            };
        var res = await UserManager.ChangePasswordAsync(user, model.oldpassword, model.newpassword);
        if (!res.Succeeded)
            return new Response
            {
                Status = "failure",
                Message = "Please check the OldPassword"
            };
        return new Response
        {
            Status = "Success",
            Message = "Action OK"
        };
    }

    /// <summary>
    ///     修改用户名
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<OneOf<Response, None>> EditUserName(EditUserNameRequest model)
    {
        if (model.username == null) return new None();
        var user = await UserManager.FindByNameAsync(model.username);
        if (user == null)
            return new Response
            {
                Status = "failure",
                Message = "User Null"
            };
        var res = await UserManager.SetUserNameAsync(user, model.newusername);
        if (!res.Succeeded)
            return new Response
            {
                Status = "failure",
                Message = "Edit failure"
            };
        return new Response
        {
            Status = "Success",
            Message = "Action OK"
        };
    }

    /// <summary>
    ///     加入一个班级
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<OneOf<Response, None>> JoinClass(JoinClassRequest model)
    {
        var res = true;
        await using (Context)
        {
            var classModel = await Context.classes.FirstOrDefaultAsync(x => x.Code == model.mycode);
            if (classModel == null)
                return new Response
                {
                    Status = "failure",
                    Message = "No this Class,Please check your code Or Connect to Admin"
                };
            var user = await UserManager.FindByNameAsync(model.username);
            if (user == null)
                return new Response
                {
                    Status = "Null",
                    Message = "Username is invalid"
                };
            user.ClassCode = classModel.Code;
            user.ClassName = classModel.Name;
            var Member = new ClassMember
            {
                Id = user.Id,
                Name = user.UserName,
                Role = RoleManager.FindByNameAsync(user.UserName).Result.Name
            };
            await Context.classMember.AddAsync(Member);
            classModel.Number += 1;
            classModel.UpdateOn = DateTime.Now;
            await Context.classMemberMapping.AddAsync(new ClassMemberMapping
            {
                ClassId = classModel.Id,
                UserId = user.Id
            });
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                res = false;
            }
        }

        return res
            ? new Response
            {
                Status = "Success",
                Message = "Save Success"
            }
            : new None();
    }
}