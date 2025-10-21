using Claims_System.Models;
using Claims_System.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core to use SQLite instead of SQL Server
builder.Services.AddDbContext<ClaimsDbContext>(options =>
    options.UseSqlite("Data Source=ClaimsDB.db"));

// Dependency Injection for ClaimService
builder.Services.AddScoped<IClaimService, ClaimService>();

// Configure session storage
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true; // security
    options.Cookie.IsEssential = true; // GDPR compliance
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // serve static files
app.UseRouting();

app.UseSession(); // required for session storage
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
