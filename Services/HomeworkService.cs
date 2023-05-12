using Microsoft.EntityFrameworkCore;
using WebAPI.IdentityExample.Contract.HomeworkContract;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Model;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Services;

public class HomeworkService : IHomeworkService
{
    private readonly SchoolDbContext _context;
    private readonly string _uri;

    public HomeworkService(SchoolDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _uri = Path.Combine(webHostEnvironment.WebRootPath, "HomeworkFiles");
    }

    public async Task<Response> CreateHomework(CreateHomeworkRequest model)
    {
        await using (_context)
        {
            var newHomework = new Homework
            {
                LasTime = DateTime.Now.AddDays(model.Time),
                Name = model.Name,
                Description = model.Detail
            };
            var url = _uri + "_" + newHomework.Id;
            newHomework.Url = url;
            var mapping = new ClassHomeworkMapping
            {
                ClassId = model.ClassId,
                HomeworkId = newHomework.Id
            };
            await using var stream = new FileStream(url, FileMode.Create);
            try
            {
                await model.File.CopyToAsync(stream);
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = "failure",
                    Message = "IO Exception"
                };
            }

            try
            {
                await _context.homeworks.AddAsync(newHomework);
                await _context.classHomeworkMapping.AddAsync(mapping);
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = "failure",
                    Message = "DbContext Exception"
                };
            }
        }

        return new Response
        {
            Status = "Success",
            Message = "Action Success"
        };
    }

    public async Task<List<ClassAllHomeworkResponse>> ClassAllHomework(ClassAllHomeworkRequest model)
    {
        await using (_context)
        {
            var result = await _context.classHomeworkMapping.Where(x => x.ClassId == model.ClassId)
                .Select(x => x.HomeworkId).ToListAsync();
            var homework = new List<ClassAllHomeworkResponse>();
            foreach (var homeworker in result)
            {
                var t = await _context.homeworks.FirstOrDefaultAsync(x => x.Id == homeworker);
                if (t != null && t.CreateOn != t.LasTime)
                    homework.Add(new ClassAllHomeworkResponse
                    {
                        HomewordName = t.Name,
                        HomewordDetail = t.Description,
                        CreateTime = t.CreateOn,
                        LasTime = t.LasTime
                    });
            }

            return homework;
        }
    }


    public async Task<Response> UpdateHomework(UpdateHomeworkRequest model)
    {
        await using (_context)
        {
            var homeworkId =
                await _context.classHomeworkMapping.FirstOrDefaultAsync(x => x.HomeworkId == model.HomeworkId);
            if (homeworkId == null)
                return new Response
                {
                    Status = "failure",
                    Message = "Please check"
                };
            var homework = await _context.homeworks.FirstOrDefaultAsync(x => x.Id == homeworkId.HomeworkId);
            var isEdit = false;
            if (DateTime.Now != homework!.LasTime)
            {
                if (model.NewHomeworkName != null)
                {
                    homework.Name = model.NewHomeworkName;
                    isEdit = true;
                }

                if (model.NewHomeworkDescription != null)
                {
                    homework.Description = model.NewHomeworkDescription;
                    isEdit = true;
                }
            }

            if (!isEdit)
                return new Response
                {
                    Status = "Success",
                    Message = "OK"
                };
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
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
            Message = "OK"
        };
    }

    public async Task<Response> DeleteHomework(DeleteHomeworkRequest model)
    {
        await using (_context)
        {
            var result = await _context.homeworks.FirstOrDefaultAsync(x => x.Id == model.HomeworkId);
            if (result == null)
                return new Response
                {
                    Status = "failure",
                    Message = "Please check homework id"
                };
            var time = result.CreateOn;
            result.LasTime = time;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new Response
                {
                    Status = "failure",
                    Message = "Delete failure"
                };
            }

            return new Response
            {
                Status = "Success",
                Message = "OK"
            };
        }
    }
}