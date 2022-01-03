using System.Collections.Generic;
using BuildingCosts.Shared.Application.Abstract;

namespace BuildingCosts.Application.Stages.GetStages;

public record GetStagesQuery : IQuery<IEnumerable<StageDto>>;