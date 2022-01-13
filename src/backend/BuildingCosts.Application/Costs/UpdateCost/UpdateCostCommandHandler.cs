using System;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Domain.ValueObjects;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using BuildingCosts.Shared.BuildingBlocks.Errors;
using OneOf;

namespace BuildingCosts.Application.Costs.UpdateCost;

public class UpdateCostCommandHandler : ICommandHandler<UpdateCostCommand, OneOf<CostSuccessfullyUpdated, Error>>
{
    private readonly ICostsRepository _costsRepository;
    private readonly IClock _clock;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCostCommandHandler(ICostsRepository costsRepository, IClock clock, IUnitOfWork unitOfWork)
    {
        _costsRepository = costsRepository;
        _clock = clock;
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<CostSuccessfullyUpdated, Error>> HandleAsync(UpdateCostCommand command)
    {
        Insist.IsNotNull(command);

        var cost = await _costsRepository.GetCostAsync(command.Id);
        if (cost is null)
        {
            return NotFoundError.Create($"Cost with id: '{command.Id}' was not found");
        }

        var utcNow = _clock.GetUtcNow();

        cost.UpdateName(command.Name);
        cost.UpdateDescription(command.Description);
        cost.UpdateStage(command.Stage);
        cost.UpdateCategory(command.Category);

        var newPositions = command.Positions
            .Select(x => Position.Create(x.Name, x.Description, x.GrossPricePerEach, x.Count, x.Unit, utcNow, x.PaymentDate))
            .ToArray();
        cost.UpdatePositions(newPositions);

        await _unitOfWork.SaveChangesAsync();

        return new CostSuccessfullyUpdated();
    }
}