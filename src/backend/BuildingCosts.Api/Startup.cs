using System;
using BuildingCosts.Api.Services;
using BuildingCosts.Application;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Infrastructure;
using BuildingCosts.Infrastructure.Repositories;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using Dawn;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BuildingCosts.Api.Startup))]

namespace BuildingCosts.Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddDbContext<CostsDbContext>(options =>
        {
            options.UseCosmos(
                Guard.Argument(Environment.GetEnvironmentVariable("CosmosDbConnectionString")).NotNull(),
                Guard.Argument(Environment.GetEnvironmentVariable("CostsDatabaseName")).NotNull());
        });

        builder.Services.Scan(selector =>
            selector.FromAssemblies(ApplicationAssembly.Assembly)
                .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        builder.Services.Scan(selector =>
            selector.FromAssemblies(ApplicationAssembly.Assembly)
                .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        builder.Services
            .AddTransient<IQueryDispatcher, QueryDispatcher>()
            .AddTransient<ICommandDispatcher, CommandDispatcher>()
            .AddScoped<ICostsRepository, CostsRepository>()
            .AddScoped<IStagesRepository, StagesRepository>()
            .AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddTransient<IClock, Clock>()
            .AddScoped<IUnitOfWork>(x => x.GetRequiredService<CostsDbContext>());
    }
}