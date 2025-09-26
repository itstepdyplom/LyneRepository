using Lyne.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromQuery] long amount)
        {
            try
            {
                var url = _stripeService.CreateCheckoutSession(
                    amount,
                    "usd",
                    "https://yourapp.com/success",
                    "https://yourapp.com/cancel"
                );

                return Ok(new { url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
