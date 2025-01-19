using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseInMemoryDatabase("HospitalDb"));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

///

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
}


app.Run();
