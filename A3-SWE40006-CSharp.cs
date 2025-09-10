using A3_SWE40006_C_.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Configure Services
// -----------------------------

// Database connection (from appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Developer-friendly DB error pages
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity (user login, roles, etc.)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;   // Email confirmation required
    options.Password.RequireDigit = true;            // Example: customize password rules
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

// MVC controllers + Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// -----------------------------
// Configure Middleware Pipeline
// -----------------------------
if (app.Environment.IsDevelopment())
{
    // Migration + detailed error pages for dev
    app.UseMigrationsEndPoint();
}
else
{
    // Global error handler for production
    app.UseExceptionHandler("/Home/Error");

    // Enforce HTTPS security headers
    app.UseHsts();  // Default = 30 days
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Identity (authentication + authorization)
app.UseAuthentication();
app.UseAuthorization();

// Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Razor Pages (used by Identity UI + custom pages)
app.MapRazorPages();

app.Run();
