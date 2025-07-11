using JewelryEstimation.Application.DTOs;
using JewelryEstimation.Application.Queries;
using JewelryEstimation.Domain;
using JewelryEstimation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryEstimation.UnitTests
{
    public class GetFinalPriceQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Correct_FinalPrice()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JewelryDb_Test1")
                .Options;

            using var context = new ApplicationDbContext(options);
            context.Jewelries.Add(new Jewelry
            {
                Id = 1,
                GoldPrice = 5000,
                Weight = 10,
                DiscountPercentage = 10
            });
            await context.SaveChangesAsync();

            var handler = new GetFinalPriceQueryHandler(context);
            var query = new GetFinalPriceQuery { JewelryId = 1 };

            // Act
            FinalPriceDto result = await handler.Handle(query, CancellationToken.None);

            // Assert
            decimal expected = (5000 * 10) - ((5000 * 10) * 10 / 100); // 50000 - 5000 = 45000
            Assert.Equal(1, result.JewelryId);
            Assert.Equal(Math.Round(expected, 2), result.FinalPrice);
        }

        [Fact]
        public async Task Handle_Throws_When_Jewelry_NotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JewelryDb_Test2")
                .Options;

            using var context = new ApplicationDbContext(options);
            var handler = new GetFinalPriceQueryHandler(context);

            var query = new GetFinalPriceQuery { JewelryId = 99 };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Contains("not found", exception.Message);
        }
    }
}
