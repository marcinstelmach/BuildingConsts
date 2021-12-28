using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Application.Costs.Commands;
using BuildingCosts.Application.Costs.Dtos;
using BuildingCosts.Application.Costs.Queries;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using OneOf;

namespace BuildingCosts.Api.Costs
{
    public class CostsFunction
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public CostsFunction(
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [FunctionName(nameof(CreateCostAsync))]
        public async Task<IActionResult> CreateCostAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "costs")]
            CreateCostRequest request)
        {
            var positionDtos = request.Positions.Select(x => new PositionDto
            {
                Name = x.Name,
                Description = x.Description,
                Count = x.Count,
                GrossPricePerEach = x.GrossPricePerEach,
                PaymentDateTime = x.PaymentDateTime,
                Unit = x.Unit
            });

            var command = new CreateCostCommand(request.Name, request.Description, request.Stage, request.Category, positionDtos);

            var result = await _commandDispatcher.DispatchCommandAsync<CreateCostCommand, OneOf<Guid, Error>>(command);

            return result.Match<IActionResult>(
                id => new OkObjectResult(new { id = id }),
                error => new StatusCodeResult(error.StatusCode));
        }

        [FunctionName(nameof(GetCostsAsync))]
        public async Task<IActionResult> GetCostsAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "costs")]
            HttpRequest request)
        {
            var query = new GetCostsQuery();

            var costs = await _queryDispatcher.DispatchQueryAsync<GetCostsQuery, IEnumerable<CostDto>>(query);
            return new OkObjectResult(costs);
        }
    }
}