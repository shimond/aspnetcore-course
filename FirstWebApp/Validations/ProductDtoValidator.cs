namespace FirstWebApp.Validations;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().NotNull();
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .Custom((value, context) => { 
                if(value % 2 != 0)
                {
                    context.AddFailure("Price is not even!");
                }
            
            });
            ; // .WithMessage("חייב להיות גדול מ0 מה נסגר?");
        RuleFor(x => x.Description).Length(5, 10);



    }
}
