namespace FirstWebApp.Services
{
    public class StripePaymentProcessor : IPaymentProcessor
    {
        public Task Pay(decimal amount)
        {
            Console.WriteLine($"Stipe payment {amount}$");
            return Task.CompletedTask;
        }
    }

    public class PaypalPaymentProcessor : IPaymentProcessor
    {
        public Task Pay(decimal amount)
        {
            Console.WriteLine($"Paypal payment {amount}$");
            return Task.CompletedTask;
        }
    }
}
