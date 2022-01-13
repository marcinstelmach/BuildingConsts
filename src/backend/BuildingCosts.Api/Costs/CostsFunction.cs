using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Application.Costs.CreateCost;
using BuildingCosts.Application.Costs.GetCost;
using BuildingCosts.Application.Costs.GetCosts;
using BuildingCosts.Application.Costs.UpdateCost;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using OneOf;
using CostDto = BuildingCosts.Application.Costs.GetCosts.CostDto;
using PositionDto = BuildingCosts.Application.Costs.GetCosts.PositionDto;

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
                PaymentDate = x.PaymentDate,
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

        [FunctionName(nameof(GetCostAsync))]
        public async Task<IActionResult> GetCostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "costs/{id:guid}")] HttpRequest request, Guid id)
        {
            var query = new GetCostByIdQuery(id);

            var result = await _queryDispatcher.DispatchQueryAsync<GetCostByIdQuery, OneOf<DetailedCostDto, Error>>(query);
            return result.Match<IActionResult>(
                dto => new OkObjectResult(dto),
                error => error switch
                {
                    NotFoundError notFoundError => new NotFoundObjectResult(new ErrorViewModel(notFoundError.Message)),
                    _ => throw new InvalidOperationException()
                });
        }

        [FunctionName(nameof(UpdateCostAsync))]
        public async Task<IActionResult> UpdateCostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "costs/{id:guid}")] UpdateCostRequest request, Guid id)
        {
            var command = new UpdateCostCommand(
                id,
                request.Name,
                request.Description,
                request.Stage,
                request.Category,
                request.Positions.Select(x => new UpdateCostCommand.PositionDto(
                    x.Name,
                    x.Description,
                    x.GrossPricePerEach,
                    x.Count,
                    x.Unit,
                    x.PaymentDate)));

            var result = await _commandDispatcher.DispatchCommandAsync<UpdateCostCommand, OneOf<CostSuccessfullyUpdated, Error>>(command);
            return result.Match<IActionResult>(
                _ => new AcceptedResult(),
                error => error switch
                {
                    BadRequestError badRequestError => new BadRequestObjectResult(new ErrorViewModel(badRequestError.Message)),
                    _ => throw new InvalidOperationException()
                });
        }
    }
}