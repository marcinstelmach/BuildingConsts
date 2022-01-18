using BuildingCosts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildingCosts.Infrastructure.Configuration;

public class CostEntityTypeConfiguration : IEntityTypeConfiguration<Cost>
{
    public void Configure(EntityTypeBuilder<Cost> builder)
    {
        builder.ToContainer("Costs");

        builder.HasIndex(x => x.Id);

        builder.HasNoDiscriminator();

        builder.HasQueryFilter(x => x.IsDeleted == false);

        builder.HasPartitionKey(x => x.Id);

        builder.OwnsOne(x => x.Category);

        builder.OwnsOne(x => x.Stage);

        builder.OwnsMany(x => x.Positions);
    }
}