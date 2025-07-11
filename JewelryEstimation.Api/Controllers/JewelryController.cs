using JewelryEstimation.Application.Commands;
using JewelryEstimation.Application.DTOs;
using JewelryEstimation.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JewelryEstimation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JewelryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JewelryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new jewelry item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateJewelry([FromBody] CreateJewelryCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFinalPrice), new { id }, new { id });
        }

        /// <summary>
        /// Gets the final price of jewelry by ID
        /// </summary>
        [HttpGet("{id}/final-price")]
        public async Task<ActionResult<FinalPriceDto>> GetFinalPrice(int id)
        {
            var query = new GetFinalPriceQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
