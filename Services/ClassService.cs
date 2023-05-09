using Microsoft.EntityFrameworkCore;
using WebAPI.IdentityExample.Contract.ClassContract;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Model;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Services
{
    /// <summary>
    ///     班级有关的服务
    /// </summary>
    public class ClassService : IClassService
    {
        private readonly SchoolDbContext _context;

        public ClassService(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     获取班级的基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ClassProfileResponse> GetClassProFile(ClassProfileRequest model)
        {
            await using (_context)
            {
                var ClassModel = await _context.classes.FirstOrDefaultAsync(x => x.Code == model.ClassCode);
                if (ClassModel is null || ClassModel.IsLockout)
                    return new ClassProfileResponse
                    {
                        Status = "failure",
                        Message = "Class is null or class has been Locked"
                    };

                var res = new ClassProfileResponse
                {
                    ClassName = ClassModel.Name,
                    ClassNumber = ClassModel.Number,
                    Description = ClassModel.Description,
                    Status = "Success",
                    Message = "Success Get"
                };
                return res;
            }
        }

        /// <summary>
        ///     创建一个班级，创建的权限由控制器分配
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> CreateClass(CreateClassRequest model)
        {
            var newClass = new Classes
            {
                Name = model.ClassName,
                Description = model.ClassDescription,
                Code = model.ClassCode
            };


            await using (_context)
            {
                var hascode = await _context.classes.AnyAsync(x => x.Code == model.ClassCode);
                if (hascode)
                    return new Response
                    {
                        Status = "Failure",
                        Message = "Please change Code"
                    };
                await _context.classes.AddAsync(newClass);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new Response
                    {
                        Status = "failure",
                        Message = "Save failure"
                    };
                }
            }

            return new Response
            {
                Status = "Success",
                Message = "Create Successed"
            };
        }

        /// <summary>
        ///     更新班级信息，根据Model中属性是否为空来判断是否要进行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> UpdateClass(ClassUpdateResquest model)
        {
            await using (_context)
            {
                var ClassModel = await _context.classes.FirstOrDefaultAsync(x => x.Code == model.ClassCode);
                if (ClassModel == null)
                    return new Response
                    {
                        Status = "failure",
                        Message = "Class null"
                    };

                if (model.Name is not null)
                    if (ClassModel.Name != model.Name)
                        ClassModel.Name = model.Name;

                if (model.Description is not null)
                    if (ClassModel.Description != model.Description)
                        ClassModel.Description = model.Description;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new Response
                    {
                        Status = "failure",
                        Message = "Save failure"
                    };
                }
            }

            return new Response
            {
                Status = "Success",
                Message = "Update Success"
            };
        }
    }
}
