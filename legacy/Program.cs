using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Radzen;
using Void.Chef.Components;
using Void.Chef.Data;
using Void.Chef.Features.Shared.Services;
using Void.Chef.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<ChatCompletionModelOptions>()
    .Bind(builder.Configuration.GetSection($"AI:{ChatCompletionModelOptions.Position}"));

builder.Services.AddKeyedSingleton<IChatCompletionService>("Chef", 
    (provider, o) => new ChatCompletionService(
        provider.GetRequiredService<IOptions<ChatCompletionModelOptions>>()));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ApplicationDbContextInitializer>();

builder.Services.AddMediatR(o => o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
} else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();