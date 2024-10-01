using ChineseStoryGenerator;
using ChineseStoryGenerator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Globalization;
using System.Net;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var appConfigConnectionString = builder.Configuration["AppConfigConnectionString"]; // Make sure this key matches what you added in the App Service settings.
if (!string.IsNullOrEmpty(appConfigConnectionString))
{
    // Add Azure App Configuration as a configuration source
    builder.Configuration.AddAzureAppConfiguration(options =>
        options.Connect(appConfigConnectionString) // Optional: For refreshing config
    );
}

//string? key = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
//string? endPoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
//string? deployment = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddScoped<IOpenAiService>(sp => new OpenAiService(key, endPoint, deployment));
builder.Services.AddScoped<IOpenAiService, OpenAiService>();
builder.Services.AddMudServices();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("zh") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var host = builder.Build();

const string defaultCulture = "en-US";

var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");
var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

if (result == null)
{
    await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
