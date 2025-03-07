namespace FirstWebApp.Contracts;

public interface IPaymentProcessor
{
    Task Pay(decimal amount);
}
