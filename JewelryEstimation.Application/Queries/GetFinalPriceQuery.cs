using JewelryEstimation.Application.DTOs;
using MediatR;
public class GetFinalPriceQuery : IRequest<FinalPriceDto>
{
    public int JewelryId { get; set; }

    public GetFinalPriceQuery() { } // 👈 Required for object initializer syntax

    public GetFinalPriceQuery(int jewelryId)
    {
        JewelryId = jewelryId;
    }
}
