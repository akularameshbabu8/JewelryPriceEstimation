using MediatR;
using System.ComponentModel.DataAnnotations;

namespace JewelryEstimation.Application.Commands
{
    public class CreateJewelryCommand : IRequest<int>
    {
        [Range(1, double.MaxValue)]
        public decimal GoldPrice { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Weight { get; set; }

        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; }

        public CreateJewelryCommand(decimal goldPrice, decimal weight, decimal discountPercentage)
        {
            GoldPrice = goldPrice;
            Weight = weight;
            DiscountPercentage = discountPercentage;
        }
    }


}