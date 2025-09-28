using DotNetTrip.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. -> Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DotNetTripDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DotNetTripDbContext>();
builder.Services.AddRazorPages();

// Todas as páginas devem ser autenticadas
builder.Services.AddRazorPages(options => {
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Conventions.AuthorizeFolder("/");
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Identity/Account/Login";   // Default login page
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Antes de UseAuthorization();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", context => {
    context.Response.Redirect("/Identity/Account/Login");
    return Task.CompletedTask;
});

app.Run();
