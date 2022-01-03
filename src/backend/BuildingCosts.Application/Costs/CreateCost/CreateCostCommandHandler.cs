using System;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.Entities;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Domain.ValueObjects;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using BuildingCosts.Shared.BuildingBlocks.Errors;
using Dawn;
using OneOf;

namespace BuildingCosts.Application.Costs.CreateCost;

public class CreateCostCommandHandler : ICommandHandler<CreateCostCommand, OneOf<Guid, Error>>
{
    private readonly IClock _clock;
    private readonly ICostsRepository _costsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCostCommandHandler(IClock clock, ICostsRepository costsRepository, IUnitOfWork unitOfWork)
    {
        _clock = clock;
        _costsRepository = costsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OneOf<Guid, Error>> HandleAsync(CreateCostCommand request)
    {
        Guard.Argument(request, nameof(request)).NotNull();

        var stage = Stage.Create(request.Stage);
        var category = Category.Create(request.Category);

        var positions = request.Positions.Select(x => Position.Create(x.Name, x.Description, x.GrossPricePerEach, x.Count, x.Unit, _clock.GetUtcNow(), x.PaymentDate)).ToArray();

        var cost = Cost.Create(request.Name, request.Description, stage, category, _clock.GetUtcNow(), positions);
        _costsRepository.AddCost(cost);

        await _unitOfWork.SaveChangesAsync();

        return cost.Id;
    }
}