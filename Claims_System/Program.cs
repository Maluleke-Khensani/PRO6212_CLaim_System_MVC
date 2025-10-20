using Claims_System.Models;
using Claims_System.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// EF Core SQL Server
builder.Services.AddDbContext<ClaimsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection for LecturerService
builder.Services.AddScoped<IClaimService, ClaimService>();

builder.Services.AddDistributedMemoryCache(); // required for session storage

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true; // security
    options.Cookie.IsEssential = true; // required for GDPR compliance
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ensure static files are served
app.UseRouting();

app.UseSession(); // <-- ADD THIS LINE HERE BEFORE app.UseAuthorization()

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
