using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Repositories.Abstract;
using AnimalStoreMvc.Repositories.Implementation;
using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);
//region localize
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IAnimaltypeService, AnimaltypeService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");


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

var suppertedCultures = new[] { "en", "tr" };
var localizationOptions=new RequestLocalizationOptions().SetDefaultCulture(suppertedCultures[1]).AddSupportedCultures(suppertedCultures).AddSupportedCultures(suppertedCultures);

app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


