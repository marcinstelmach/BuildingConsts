using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Api.ViewModels;
using BuildingCosts.Application.Costs.Commands;
using BuildingCosts.Application.Costs.Queries;
using BuildingCosts.Application.Dtos;
using BuildingCosts.Shared.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace BuildingCosts.Api
{
    public class CostsFunction
    {
        private readonly IHandlerDispatcher _dispatcher;

        public CostsFunction(IHandlerDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [FunctionName(nameof(CreateCostAsync))]
        public async Task<IActionResult> CreateCostAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "costs")]
            CreateCostViewModel viewModel)
        {
            var positionDtos = viewModel.Positions.Select(x => new PositionDto
            {
                Name = x.Name,
                Description = x.Description,
                Count = x.Count,
                GrossPricePerEach = x.GrossPricePerEach,
                PaymentDateTime = x.PaymentDateTime,
                Unit = x.Unit
            });

            var command = new CreateCostCommand(viewModel.Name, viewModel.Description, viewModel.Stage, viewModel.Category, positionDtos);

            await _dispatcher.DispatchCommandAsync(command);

            return new OkResult();
        }

        [FunctionName(nameof(GetCostsAsync))]
        public async Task<IActionResult> GetCostsAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "costs")]
            HttpRequest request)
        {
            var query = new GetCostsQuery();

            var costs = await _dispatcher.DispatchQueryAsync<GetCostsQuery, IEnumerable<CostDto>>(query);
            return new OkObjectResult(costs);
        }
    }
}