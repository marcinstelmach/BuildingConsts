using System.Collections.Generic;
using BuildingCosts.Application.Stages.Dtos;
using BuildingCosts.Shared.Application.Abstract;

namespace BuildingCosts.Application.Stages.Queries;

public record GetStagesQuery : IQuery<IEnumerable<StageDto>>;