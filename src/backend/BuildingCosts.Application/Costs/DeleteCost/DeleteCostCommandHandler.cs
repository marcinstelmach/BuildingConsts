using System.Threading.Tasks;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using OneOf;

namespace BuildingCosts.Application.Costs.DeleteCost;

public class DeleteCostCommandHandler : ICommandHandler<DeleteCostCommand, OneOf<CostDeletedSuccessfully, CostNotFound>>
{
    private readonly ICostsRepository _costsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCostCommandHandler(ICostsRepository costsRepository, IUnitOfWork unitOfWork)
    {
        _costsRepository = costsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<CostDeletedSuccessfully, CostNotFound>> HandleAsync(DeleteCostCommand command)
    {
        Insist.IsNotNull(command);

        var cost = await _costsRepository.GetCostAsync(command.CostId);
        if (cost is null)
        {
            return new CostNotFound();
        }
        
        cost.DeleteCost();
        await _unitOfWork.SaveChangesAsync();

        return new CostDeletedSuccessfully();
    }
}