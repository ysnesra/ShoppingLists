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

//AutofacServisi kullan demi� olduk
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.
builder.Services.AddControllersWithViews().ConfigureApiBehaviorOptions(x=>x.SuppressModelStateInvalidFilter=true).AddRazorRuntimeCompilation();


//builder.Services.AddFluentValidation(a => a.RegisterValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()));
//builder.Services.AddFluentValidation(a => a.RegisterValidatorsFromAssemblyContaining<UserRegisterValidator>());
builder.Services.AddBusinessService();   //.net in kendi Ioc Container�n� kulllan�r�z.//DependencyEnjection.cs 'de olu�turugum metotu ekliyorum

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = ".ShoppingLists.auth";  //Kullan�c�n�n taray�c�s�nda cookie bilgilerini bu isimde sakla
        opts.ExpireTimeSpan = TimeSpan.FromDays(20);     //ne kadar s�re sonra bu cookie ge�ersiz olacak. 7 g�nde 1 yenilensin
        opts.LoginPath = "/User/Login";   //Cookie yi bulamazsa LoginPath e y�nledirmesi gerekiyor//Login de�ilse buraya atacak
        opts.LogoutPath = "/User/Logout";  //��k�� yapma
        opts.AccessDeniedPath = "/Home/AccessDenied";   //Yetkisi olmad���nda gidece�i sayfa//Rol� uymuyorsa bu sayfaya at�yor
    });

//***Http yap�s�n� b�t�n katmanalarda kullanmam�z� sa�lar*******
builder.Services.AddHttpContextAccessor();


//Autofac yap�s�na ta��yaca��z //Autofac => IocContainer alt yap�s� sunuyor
//AutofacBusinessModule.cs clas�nda ayn� ili�kilendirmeyi yapar�z 
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
