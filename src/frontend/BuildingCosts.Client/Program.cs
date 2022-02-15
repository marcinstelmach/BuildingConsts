using System;
using System.Net.Http;
using BuildingCosts.Client;
using BuildingCosts.Client.Services.Categories;
using BuildingCosts.Client.Services.Costs;
using BuildingCosts.Client.Services.Stages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// builder.Services.AddApiClients(builder.Configuration);

var apiConnectionString = builder.Configuration["ApiConnectionString"];
if (apiConnectionString is null)
{
    throw new InvalidOperationException("Empty ApiConnectionString !!!");
}

builder.Services.AddRefitClient<ICostsApiClient>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri(apiConnectionString));

builder.Services.AddRefitClient<IStagesApiClient>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri(apiConnectionString));

builder.Services.AddRefitClient<ICategoriesApiClient>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri(apiConnectionString));

builder.Services.AddScoped<ICostsService, CostsService>();
builder.Services.AddScoped<IStagesService, StagesService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

await builder.Build().RunAsync();
