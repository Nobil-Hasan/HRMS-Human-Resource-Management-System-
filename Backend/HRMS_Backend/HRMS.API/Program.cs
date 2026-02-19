using HRMS.DAL.Data;
using HRMS.DAL.Interfaces;
using HRMS.DAL.Repositories;
using HRMS.BLL.Interfaces;
using HRMS.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();

// 2. Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Configure JWT Authentication Services
// This fulfills the requirement for implementing security measures to protect sensitive data.
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 4. Register Repositories for Dependency Injection (DAL)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// 5. Register Business Logic Services (BLL)
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISalaryService, SalaryService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add this before builder.Build()
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// 6. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Add this before app.UseAuthorization()
app.UseCors("AllowAngular");

// 7. Security Middleware
// Authentication must come before Authorization to ensure secure access to HRMS functionalities.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();