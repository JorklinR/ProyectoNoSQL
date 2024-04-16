using System;
using NoSQL_Proyecto.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoSQL_Proyecto.Data;
using NoSQL_Proyecto.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Conection"));

builder.Services.AddSingleton<ArticulosService>();
builder.Services.AddSingleton<UsuarioService>();
builder.Services.AddSingleton<IngresosService>();
builder.Services.AddSingleton<MetodoPagoService>();
builder.Services.AddSingleton<MotivoSalidaService>();
builder.Services.AddSingleton<ProveedorService>();
builder.Services.AddSingleton<SalidasService>();
builder.Services.AddSingleton<TipoArticulosService>();
builder.Services.AddSingleton<TipoProveedoresService>();
builder.Services.AddSingleton<TipoUsuariosService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // La ruta a la página de inicio de sesión
    });

// Agregar servicios de Razor Pages
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.MapRazorPages();

// Imprimir un mensaje de confirmación en la consola
Console.WriteLine("¡La aplicación se conectó correctamente a la base de datos!");

app.Run();
