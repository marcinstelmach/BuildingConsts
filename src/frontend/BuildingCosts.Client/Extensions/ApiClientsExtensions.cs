using System;
using BuildingCosts.Client.Services.Categories;
using BuildingCosts.Client.Services.Costs;
using BuildingCosts.Client.Services.Stages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BuildingCosts.Client.Extensions;

public static class ApiClientsExtensions
{
    public static IServiceCollection AddApiClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiConnectionString = configuration["ApiConnectionString"];
        if (apiConnectionString is null)
        {
            throw new InvalidOperationException("Empty ApiConnectionString !!!");
        }

        services.AddRefitClient<ICostsApiClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(apiConnectionString));

        services.AddRefitClient<IStagesApiClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(apiConnectionString));
        
        services.AddRefitClient<ICategoriesApiClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(apiConnectionString));

        return services;
    }
}