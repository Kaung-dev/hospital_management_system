using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI for the database context
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseInMemoryDatabase("HospitalDb"));

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
    options.Cookie.HttpOnly = true; // Make the session cookie accessible only via HTTP
    options.Cookie.IsEssential = true; // Mark the cookie as essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add session middleware
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

    // Seed Departments
    if (!context.Departments.Any())
    {
        context.Departments.AddRange(
            new Department { Name = "Cardiology", app_price = 200, Description = "Heart specialists" },
            new Department { Name = "Neurology", app_price = 250, Description = "Brain and nerve specialists" }
        );
        context.SaveChanges();
    }

    // Seed Patients
    if (!context.Patients.Any())
    {
        context.Patients.AddRange(
            new Patient { Name = "John Doe", Age = 45, Diagnosis = "Hypertension" },
            new Patient { Name = "Jane Smith", Age = 30, Diagnosis = "Diabetes" }
        );
        context.SaveChanges();
    }

    // Seed Users
    if (!context.Users.Any())
    {
        var passwordHasher = new PasswordHasher<string>();
        context.Users.AddRange(
            new User
            {
                Name = "Admin",
                Email = "admin@example.com",
                Password = passwordHasher.HashPassword(null, "Admin@123"),
                //Role = "Admin",
                Phone = "1234567890"
            },
            new User
            {
                Name = "Doctor John",
                Email = "doctor@example.com",
                Password = passwordHasher.HashPassword(null, "Doctor@123"),
                //Role = "Doctor",
                Phone = "9876543210"
            }
        );
        context.SaveChanges();
    }

}

app.Run();
