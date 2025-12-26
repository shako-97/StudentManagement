using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Profiles;
using StudentManagement.Application.Shared.Behaviour;
using StudentManagement.Application.Shared.Middlewares;
using StudentManagement.Application.StudentManagement.Commands.CreateStudent;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Interfaces.Respositories;
using StudentManagement.Infrastructure;
using StudentManagement.Infrastructure.DataAccess;
using StudentManagement.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateStudentCommand>();
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateStudentCommandValidator).GetTypeInfo().Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

//builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<StudentProfile>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();
