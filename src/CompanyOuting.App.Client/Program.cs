using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CompanyOuting.App.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<UserSessionService>();

await builder.Build().RunAsync();
