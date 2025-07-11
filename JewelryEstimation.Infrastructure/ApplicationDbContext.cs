using JewelryEstimation.Application.Interfaces;
using JewelryEstimation.Domain;
using Microsoft.EntityFrameworkCore;

namespace JewelryEstimation.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Jewelry> Jewelries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Jewelry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.GoldPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Weight).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5,2)");
            });
        }
    }
}

