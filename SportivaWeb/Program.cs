using Microsoft.FluentUI.AspNetCore.Components;
using SportivaWeb.Components;
using SportivaWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

builder.Services.AddSingleton<ISesionService, SesionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
