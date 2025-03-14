using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DestinyForum.Data;
using Microsoft.AspNetCore.Identity;
using DestinyForum.Models;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<DestinyForumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DestinyForumContext") ?? throw new InvalidOperationException("Connection string 'DestinyForumContext' not found.")));

// Add Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DestinyForumContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages().WithStaticAssets();

app.Run();
