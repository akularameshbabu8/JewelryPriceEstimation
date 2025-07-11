using JewelryEstimation.Application.Commands;
using JewelryEstimation.Application.Interfaces;
using JewelryEstimation.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryEstimation.UnitTests
{
    public class CreateJewelryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_Jewelry_And_Return_Id()
        {
            // Arrange
            var jewelryList = new List<Jewelry>();
            var mockSet = new Mock<DbSet<Jewelry>>();

            // Setup mock DbSet to behave like a real one
            mockSet.Setup(d => d.Add(It.IsAny<Jewelry>()))
                   .Callback<Jewelry>(j => { j.Id = 1; jewelryList.Add(j); });

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Jewelries).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(1);

            var handler = new CreateJewelryCommandHandler(mockContext.Object);

            var command = new  CreateJewelryCommand(6000, 10, 10);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
            Assert.Single(jewelryList);
            Assert.Equal(6000, jewelryList[0].GoldPrice);
            Assert.Equal(10, jewelryList[0].Weight);
            Assert.Equal(10, jewelryList[0].DiscountPercentage);

            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}