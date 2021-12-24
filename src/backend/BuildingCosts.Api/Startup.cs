using System;
using System.Collections.Generic;
using BuildingCosts.Application.Costs.Commands;
using BuildingCosts.Application.Costs.Queries;
using BuildingCosts.Application.Dtos;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Infrastructure;
using BuildingCosts.Infrastructure.Repositories;
using BuildingCosts.Shared.Application;
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

        builder.Services.AddTransient<IHandlerDispatcher, HandlerDispatcher>();
        builder.Services.AddTransient<ICommandHandler<CreateCostCommand>, CreateCostCommandHandler>();
        builder.Services.AddTransient<IQueryHandler<GetCostsQuery, IEnumerable<CostDto>>, GetCostsQueryHandler>();

        builder.Services.AddScoped<ICostsRepository, CostsRepository>();
        builder.Services.AddTransient<IClock, Clock>();
        builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<CostsDbContext>());
    }
}