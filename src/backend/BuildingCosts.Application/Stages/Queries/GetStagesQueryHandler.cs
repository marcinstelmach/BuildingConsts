using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Application.Stages.Dtos;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Shared.Application.Abstract;
using Dawn;

namespace BuildingCosts.Application.Stages.Queries;

public class GetStagesQueryHandler : IQueryHandler<GetStagesQuery, IEnumerable<StageDto>>
{
    private readonly IStagesRepository _stagesRepository;

    public GetStagesQueryHandler(IStagesRepository stagesRepository)
    {
        _stagesRepository = stagesRepository;
    }

    public async Task<IEnumerable<StageDto>> HandleAsync(GetStagesQuery query)
    {
        Guard.Argument(query, nameof(query)).NotNull();

        var stages = await _stagesRepository.GetStagesAsync();
        return stages.Select(x => new StageDto(x.Name));
    }
}