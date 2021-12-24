using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Domain.ValueObjects;

namespace BuildingCosts.Domain.Repositories;

public interface IPositionsRepository
{
    Task<IEnumerable<Position>> GetPositionsAsync();
}