namespace FirstWebApp.EndPoints;

public static class PaymentsEndPoints
{

    public static void MapPayments(this WebApplication app)
    {
        var paymentsGroup = app.MapGroup("payments").WithTags("Payments");

        paymentsGroup.MapPost("", Pay).WithName(nameof(Pay));
    }

    static async Task<Results<Ok<string>, NotFound>> Pay(PaymentRequestDto dto, IServiceProvider serviceProvider)
    {
        var processor = serviceProvider.GetKeyedService<IPaymentProcessor>(dto.paymentType);
        if (processor is not null)
        {
            await processor.Pay(dto.Amount);
            return TypedResults.Ok("OK!");
        }
        else
        {
            return TypedResults.NotFound(); // conflict
        }
    }

}
