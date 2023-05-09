using Microsoft.EntityFrameworkCore;
using WebAPI.IdentityExample.Model;

namespace WebAPI.IdentityExample.DAL;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    public DbSet<Homework> homeworks { get; set; }
    public DbSet<Classes> classes { get; set; }
    public DbSet<ClassHomeworkMapping> classHomeworkMapping { get; set; }
    public DbSet<ClassMember> classMember { get; set; }

    public DbSet<ClassMemberMapping> classMemberMapping { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        //多对多关系构建
        builder.Entity<ClassHomeworkMapping>(entity =>
        {
            entity.HasKey(x => new
            {
                x.ClassId,
                x.HomeworkId
            });

            //homework与ClassHomeworkMapping的一对多
            entity.HasOne(x => x.homework)
                .WithMany(x => x.ClassHomeworkMappings)
                .HasForeignKey(x => x.HomeworkId)
                .OnDelete(DeleteBehavior.Restrict);

            //Class与ClassHomeworkMapping的一对多
            entity.HasOne(x => x.Class)
                .WithMany(x => x.ClassHomeworkMappings)
                .HasForeignKey(x => x.ClassId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ClassMemberMapping>()
            .HasKey(c => new { c.ClassId, c.UserId });

        builder.Entity<ClassMemberMapping>()
            .HasOne(c => c.Class)
            .WithMany(c => c.MemberMappings)
            .HasForeignKey(c => c.ClassId);

        builder.Entity<ClassMemberMapping>()
            .HasOne(c => c.User)
            .WithMany(c => c.ClassesMappings)
            .HasForeignKey(c => c.UserId);
    }
}