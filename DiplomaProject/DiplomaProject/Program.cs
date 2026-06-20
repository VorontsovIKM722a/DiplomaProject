using DiplomaProject.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DiplomaProject.Services;
using DiplomaProject.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<TestGeneration>();
builder.Services.AddScoped<TabItemService>();
builder.Services.AddScoped<SpotlightService>();
builder.Services.AddScoped<TestEvaluationService>();

builder.Services.AddScoped<TestStatisticsService>();
builder.Services.AddScoped<AntiCheatService>();

builder.Services.AddScoped<SaveTestService>();
builder.Services.AddScoped<LoadTestService>();
builder.Services.AddScoped<DeleteTestService>();

builder.Services.AddSingleton<JsonDataService>();
builder.Services.AddSingleton<GeminiPdfService>();
builder.Services.AddSingleton<GeminiTestGeneration>();
builder.Services.AddSingleton<GeminiTestGenerationLocal>();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();
   

app.Run();
