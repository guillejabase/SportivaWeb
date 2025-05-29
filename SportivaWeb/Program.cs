using Microsoft.FluentUI.AspNetCore.Components;
using SportivaWeb.Components;
using SportivaWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IDialogService, DialogService>();
builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IInscripcionesService, InscripcionesService>();
builder.Services.AddScoped<IInscriptosService, InscriptosService>();
builder.Services.AddScoped<IProvinciasService, ProvinciasService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<ISexosService, SexosService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

builder.Services.AddSingleton<ISesionService, SesionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
