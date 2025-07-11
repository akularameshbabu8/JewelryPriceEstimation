using JewelryEstimation.Domain;
using Microsoft.EntityFrameworkCore;

namespace JewelryEstimation.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Jewelry> Jewelries { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}