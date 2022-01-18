using System;
using BuildingCosts.Shared.Application.Abstract;
using OneOf;

namespace BuildingCosts.Application.Costs.DeleteCost;

public class DeleteCostCommand : ICommand<OneOf<CostDeletedSuccessfully, CostNotFound>>
{
    public DeleteCostCommand(Guid costId)
    {
        CostId = costId;
    }

    public Guid CostId { get; }
}