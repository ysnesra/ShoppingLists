using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.Extentions;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//AutofacServisi kullan demiþ olduk
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.
builder.Services.AddControllersWithViews().ConfigureApiBehaviorOptions(x=>x.SuppressModelStateInvalidFilter=true).AddRazorRuntimeCompilation();


//builder.Services.AddFluentValidation(a => a.RegisterValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()));
//builder.Services.AddFluentValidation(a => a.RegisterValidatorsFromAssemblyContaining<UserRegisterValidator>());
builder.Services.AddBusinessService();   //.net in kendi Ioc Containerýný kulllanýrýz.//DependencyEnjection.cs 'de oluþturugum metotu ekliyorum

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = ".ShoppingLists.auth";  //Kullanýcýnýn tarayýcýsýnda cookie bilgilerini bu isimde sakla
        opts.ExpireTimeSpan = TimeSpan.FromDays(20);     //ne kadar süre sonra bu cookie geçersiz olacak. 7 günde 1 yenilensin
        opts.LoginPath = "/User/Login";   //Cookie yi bulamazsa LoginPath e yönledirmesi gerekiyor//Login deðilse buraya atacak
        opts.LogoutPath = "/User/Logout";  //Çýkýþ yapma
        opts.AccessDeniedPath = "/Home/AccessDenied";   //Yetkisi olmadýðýnda gideceði sayfa//Rolü uymuyorsa bu sayfaya atýyor
    });

//***Http yapýsýný bütün katmanalarda kullanmamýzý saðlar*******
builder.Services.AddHttpContextAccessor();


//Autofac yapýsýna taþýyacaðýz //Autofac => IocContainer alt yapýsý sunuyor
//AutofacBusinessModule.cs clasýnda ayný iliþkilendirmeyi yaparýz 
//builder.Services.AddSingleton<IProductService,ProductManager>();
//builder.Services.AddSingleton<IProductDal,EfProductDal>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
