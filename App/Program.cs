using App.BLL;
using App.DAL;
using App.Data;
using App.IDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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


// Add Service : Swagger 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Identitys App API",
        Description = "Identitys.App.APIS",
        Contact = new OpenApiContact
        {
            Name = "yeong-hyeon-kim",
            Email = string.Empty,
            Url = new Uri("https://github.com/yeong-hyeon-kim")
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});

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

// Use Swagger
app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.APIS");
    c.RoutePrefix = "swagger";
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
