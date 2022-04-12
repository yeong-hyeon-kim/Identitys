using App.BLL;
using App.DAL;
using App.Data;
using App.IDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionApp = builder.Configuration.GetConnectionString("APP.DB");
var connectionAppIdentity = builder.Configuration.GetConnectionString("APP.IDENTITY");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionApp));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionAppIdentity));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<AppLogic>();
builder.Services.AddTransient<IAppDal, AppDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
