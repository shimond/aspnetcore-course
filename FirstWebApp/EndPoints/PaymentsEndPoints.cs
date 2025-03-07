namespace FirstWebApp.EndPoints;

public static class PaymentsEndPoints
{

    public static void MapPayments(this WebApplication app)
    {
        var paymentsGroup = app.MapGroup("payments").WithTags("Payments");

        paymentsGroup.MapPost("", Pay).WithName(nameof(Pay));
    }

    static async Task<Ok<string>> Pay(PaymentRequestDto dto, [FromKeyedServices("paypal")]IPaymentProcessor processor)
    {
        await processor.Pay(dto.Amount);
        return TypedResults.Ok("OK!");
    }

}
