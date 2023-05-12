using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.DAL;
using WebAPI.IdentityExample.Model;
using WebAPI.IdentityExample.Services;
using WebAPI.IdentityExample.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AuthDB
builder.Services.AddDbContext<AuthDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB"))
    );

//DataDB
builder.Services.AddDbContext<SchoolDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataDB"))
);

// ASP.Net Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// JWTService
builder.Services.AddOptions<JWTServiceOptions>()
    .Bind(builder.Configuration.GetRequiredSection(JWTServiceOptions.JWTService))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IJWTService, JWTService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddScoped<IAdminService, AdminService>();
// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// JWT Bearer
.AddJwtBearer(options =>
{
    var jwtOpts = builder.Configuration
        .GetRequiredSection(JWTServiceOptions.JWTService)
        .Get<JWTServiceOptions>();

    var secretBytes = Encoding.UTF8.GetBytes(jwtOpts!.Secret);

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtOpts!.ValidIssuer,
        ValidAudience = jwtOpts!.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Clean database after each startup
    var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();

    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    await authService.SeedRoles();
    var model = new RegisterModel
    {
        Username = "Admin",
        Password = "W0rld!W0rld!",
        Email = "hello@world.com"
    };
    Console.WriteLine($"name == {model.Username}");
    await authService.Register(model, UserRole.Admin);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

