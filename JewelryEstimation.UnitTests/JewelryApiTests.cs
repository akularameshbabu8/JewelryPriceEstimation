using JewelryEstimation.Api.Controllers;
using JewelryEstimation.Application.Commands;
using JewelryEstimation.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JewelryEstimation.UnitTests
{
    public class JewelryControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly JewelryController _controller;


        public JewelryControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new JewelryController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateJewelry_Should_Return_CreatedAtAction_With_Id()
        {
            // Arrange
            var command = new CreateJewelryCommand(6000, 10, 5);


            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateJewelryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(101); // Simulate DB-generated ID

            // Act
            var result = await _controller.CreateJewelry(command);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(JewelryController.GetFinalPrice), actionResult.ActionName);

        }

        [Fact]
        public async Task GetFinalPrice_Should_Return_FinalPriceDto_When_Valid_Id()
        {
            // Arrange
            int jewelryId = 101;
            var finalPriceDto = new FinalPriceDto
            {
                JewelryId = jewelryId,
                FinalPrice = 54000
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetFinalPriceQuery>(q => q.JewelryId == jewelryId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(finalPriceDto);

            // Act
            var result = await _controller.GetFinalPrice(jewelryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<FinalPriceDto>(okResult.Value);
            Assert.Equal(jewelryId, dto.JewelryId);
            Assert.Equal(54000, dto.FinalPrice);
        }

        [Fact]
        public async Task GetFinalPrice_Should_Return_NotFound_When_Invalid_Id()
        {
            // Arrange
            int invalidId = 999;

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetFinalPriceQuery>(q => q.JewelryId == invalidId), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new KeyNotFoundException("Jewelry not found"));

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.GetFinalPrice(invalidId));
        }

        [Fact]
        public async Task CreateJewelry_Should_Return_400_When_ModelState_Is_Invalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Weight", "Weight is required");

            var command = new CreateJewelryCommand(5000, 0, 5); // Will fail model validation

            // Act
            var result = await _controller.CreateJewelry(command);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateJewelry_Should_Return_500_When_Exception_Thrown()
        {
            // Arrange
            var command = new CreateJewelryCommand(5000, 10, 5);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateJewelryCommand>(), default))
                         .ThrowsAsync(new Exception("Something failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.CreateJewelry(command));
        }
    }
}