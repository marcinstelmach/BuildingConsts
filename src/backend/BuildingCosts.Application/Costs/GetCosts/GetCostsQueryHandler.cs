using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Shared.Application.Abstract;
using Dawn;

namespace BuildingCosts.Application.Costs.GetCosts;

public class GetCostsQueryHandler : IQueryHandler<GetCostsQuery, IEnumerable<CostDto>>
{
    private readonly ICostsRepository _costsRepository;

    public GetCostsQueryHandler(ICostsRepository costsRepository)
    {
        _costsRepository = costsRepository;
    }

    public async Task<IEnumerable<CostDto>> HandleAsync(GetCostsQuery query)
    {
        Guard.Argument(query, nameof(query)).NotNull();

        var costs = await _costsRepository.GetCostsAsync();
        return costs.Select(x => new CostDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Category = x.Category.Name,
            Stage = x.Stage.Name,
            GrossPrice = x.GrossPrice,
            IsPayed = x.IsPayed,
            CreationDateTime = x.CreationDateTime,
            Positions = x.Positions.Select(y => new PositionDto
            {
                Name = y.Name,
                Description = y.Description,
                Count = y.Count,
                Unit = y.Unit,
                PaymentDate = y.PaymentDate,
                GrossPricePerEach = y.GrossPricePerEach
            })
        });
    }
}