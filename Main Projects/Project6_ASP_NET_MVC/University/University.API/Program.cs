using Microsoft.EntityFrameworkCore;
using System;
using University.Application.Services;
using University.Application.Services.Base;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext to the services container
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connectionString));

//base repos
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped<IWaitlistEntryService, WaitlistEntryService>();
builder.Services.AddScoped<IEnrollmentsService, EnrollmentsService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICoursePrerequisiteService, CoursePrerequisiteService>();
builder.Services.AddScoped<ICourseSectionService, CourseSectionService>();
builder.Services.AddScoped<IDegreeRequirementService, DegreeRequirementService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDegreeProgramService, DegreeProgramService>();

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
