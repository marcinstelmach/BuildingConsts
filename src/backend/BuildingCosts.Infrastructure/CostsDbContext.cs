using System.Threading.Tasks;
using BuildingCosts.Domain.Entities;
using BuildingCosts.Infrastructure.Configuration;
using BuildingCosts.Shared.BuildingBlocks;
using Microsoft.EntityFrameworkCore;

namespace BuildingCosts.Infrastructure;

public class CostsDbContext : DbContext, IUnitOfWork
{
    public CostsDbContext(DbContextOptions<CostsDbContext> options)
    : base(options)
    {
    }

    public DbSet<Cost> Costs { get; set; }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CostEntityTypeConfiguration());
    }
}