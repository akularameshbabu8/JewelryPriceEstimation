using MediatR;
using JewelryEstimation.Application.DTOs;
using JewelryEstimation.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelryEstimation.Application.Queries
{
    public class GetFinalPriceQueryHandler : IRequestHandler<GetFinalPriceQuery, FinalPriceDto>
    {
        private readonly IApplicationDbContext _context;

        public GetFinalPriceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinalPriceDto> Handle(GetFinalPriceQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Jewelries.FindAsync(request.JewelryId);
            if (item == null) throw new KeyNotFoundException($"Jewelry {request.JewelryId} not found");

            var basePrice = item.GoldPrice * item.Weight;
            var discount = basePrice * item.DiscountPercentage / 100;
            var final = basePrice - discount;

            return new FinalPriceDto
            {
                JewelryId = item.Id,
                FinalPrice = Math.Round(final, 2)
            };
        }
    }
}