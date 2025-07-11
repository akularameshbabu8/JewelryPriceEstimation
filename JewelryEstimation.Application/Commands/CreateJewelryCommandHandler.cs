using MediatR;
using JewelryEstimation.Domain;
using JewelryEstimation.Application.Interfaces;

namespace JewelryEstimation.Application.Commands
{
    public class CreateJewelryCommandHandler : IRequestHandler<CreateJewelryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateJewelryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateJewelryCommand request, CancellationToken cancellationToken)
        {
            var jewelry = new Jewelry
            {
                GoldPrice = request.GoldPrice,
                Weight = request.Weight,
                DiscountPercentage = request.DiscountPercentage
            };

            _context.Jewelries.Add(jewelry);
            await _context.SaveChangesAsync(cancellationToken);
            return jewelry.Id;
        }
    }
}