namespace Travel_project.Models.Stripe
{
    public record AddStripeCard
    (
        string Cvc,
        string Name,
        double amount,
        string userId,
        string CardNumber,
        string ExpirationYear,
        string ExpirationMonth

    );
}
