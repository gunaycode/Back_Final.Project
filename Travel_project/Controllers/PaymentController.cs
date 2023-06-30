using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Travel_project.Models.Stripe;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Payment(AddStripeCard stripeCard)
        {
            StripeConfiguration.ApiKey = "sk_test_51NOjqHBLzldeeaXproeaOo7Mect5FIuOibGZBk4j2bf5zaPUkWtoGnSaeVKERexniBfBPm3Tlk04NZZfhAHNjivE001zMGn9YN";

            TokenCreateOptions tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = stripeCard.Name,
                    Number = stripeCard.CardNumber,
                    ExpYear = stripeCard.ExpirationYear,
                    ExpMonth = stripeCard.ExpirationMonth,
                    Cvc = stripeCard.Cvc
                }
            };

            var serviceToken = new TokenService();
            Token stripeToken = await serviceToken.CreateAsync(tokenOptions);

            var options = new ChargeCreateOptions
            {
                Amount = (long)stripeCard.amount * 100,
                Currency = "usd",
                Description = "test",
                Source = stripeToken.Id
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            if (charge.Paid)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(500);
            }



        }
    }
}
