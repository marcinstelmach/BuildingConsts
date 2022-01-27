using BuildingCosts.Client;
using BuildingCosts.Client.Costs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddRefitClient<ICostsApiClient>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri(builder.Configuration["ApiConnectionString"] ?? throw new InvalidOperationException("Missing api connectionString")));

builder.Services.AddScoped<ICostsService, CostsService>();

await builder.Build().RunAsync();