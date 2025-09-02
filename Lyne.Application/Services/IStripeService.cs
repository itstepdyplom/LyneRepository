namespace Lyne.Application.Services
{
    public interface IStripeService
    {
        string CreateCheckoutSession(long amount, string currency, string successUrl, string cancelUrl);
    }
}
