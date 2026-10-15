using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Repositories;
using University.Repositories.Base;
using University.Services;
using University.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Connection string for db
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext to the services container
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connectionString));

// ================================ 
// Cookie Authentication 
// ================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";
        //options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Repositories
builder.Services.AddScoped<IWaitlistEntryRepository, WaitlistEntryRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICoursePrerequisiteRepository, CoursePrerequisiteRepository>();
builder.Services.AddScoped<ICourseSectionRepository, CourseSectionRepository>();
builder.Services.AddScoped<IDegreeRequirementRepository, DegreeRequirementRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IDegreeProgramRepository, DegreeProgramRepository>();


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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

app.Run();
