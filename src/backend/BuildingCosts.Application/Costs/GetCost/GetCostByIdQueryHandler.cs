using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using BuildingCosts.Shared.BuildingBlocks.Errors;
using OneOf;

namespace BuildingCosts.Application.Costs.GetCost;

public class GetCostByIdQueryHandler : IQueryHandler<GetCostByIdQuery, OneOf<DetailedCostDto, Error>>
{
    private readonly ICostsRepository _costsRepository;

    public GetCostByIdQueryHandler(ICostsRepository costsRepository)
    {
        _costsRepository = costsRepository;
    }

    public async Task<OneOf<DetailedCostDto, Error>> HandleAsync(GetCostByIdQuery byIdQuery)
    {
        Insist.IsNotNull(byIdQuery);

        var cost = await _costsRepository.GetCostAsync(byIdQuery.Id);
        if (cost is null)
        {
            return NotFoundError.Create($"Cost with id: '{byIdQuery.Id}' was not found");
        }

        var costDto = new DetailedCostDto
        {
            Id = cost.Id,
            Name = cost.Name,
            Description = cost.Description,
            Category = cost.Category.Name,
            Stage = cost.Stage.Name,
            GrossPrice = cost.GrossPrice,
            IsPayed = cost.IsPayed,
            CreationDateTime = cost.CreationDateTime,
            Positions = cost.Positions.Select(y => new PositionDto
            {
                Name = y.Name,
                Description = y.Description,
                Count = y.Count,
                Unit = y.Unit,
                CreationDateTime = y.CreationDateTime,
                PaymentDate = y.PaymentDate,
                GrossPricePerEach = y.GrossPricePerEach
            })
        };

        return costDto;
    }
}