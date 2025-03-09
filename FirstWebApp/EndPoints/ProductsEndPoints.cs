namespace FirstWebApp.EndPoints;

public static class ProductsEndPoints
{

    public static void MapProducts(this WebApplication app)
    {
        var productsGroup = app.MapGroup("products").WithTags("Products");

        productsGroup.MapGet("", GetAllProducts).WithName(nameof(GetAllProducts));

        productsGroup.MapGet("{id}", GetProductById).WithName(nameof(GetProductById));

        productsGroup.MapDelete("{id}", DeleteItem).WithName(nameof(DeleteItem));

        productsGroup.MapPut("{id}", UpdateProduct).WithName(nameof(UpdateProduct));

        productsGroup.MapPost("", AddNewProduct).WithName(nameof(AddNewProduct)).RequireAuthorization();
    }

    static async Task<Ok<List<ProductDto>>> GetAllProducts(IProductsRepository repository, IMapper mapper)
    {
        var result = await repository.GetAllProducts();
        var mappedResult = mapper.Map<List<ProductDto>>(result);
        return TypedResults.Ok(mappedResult);
    }

    static async Task<Results<NotFound, Ok<ProductDto>>> GetProductById(int id, IProductsRepository repository, IMapper mapper)
    {
        var result = await repository.GetProductById(id);
        if (result is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<ProductDto>(result));
    }

    static async Task<Results<NotFound, Ok<ProductDto>>> DeleteItem(int id, IProductsRepository repository, IMapper mapper)
    {
        var result = await repository.GetProductById(id);
        if (result is null)
        {
            return TypedResults.NotFound();
        }
        await repository.DeleteItem(id);
        return TypedResults.Ok(mapper.Map<ProductDto>(result));
    }


    static async Task<Results<NotFound, Ok<ProductDto>>> UpdateProduct(int id, ProductEntity product, IProductsRepository repository, IMapper mapper)
    {
        var result = await repository.GetProductById(id);
        if (result is null)
        {
            return TypedResults.NotFound();
        }

        var productAfterUpdate = await repository.UpdateProduct(id, product);
        return TypedResults.Ok(mapper.Map<ProductDto>(productAfterUpdate));
    }


    static async Task<Results<Created<ProductDto>, ValidationProblem>> AddNewProduct(ProductDto product, 
        IValidator<ProductDto> validator,
        IProductsRepository repository, 
        IMapper mapper)
    {
        var validationResult = validator.Validate(product);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }
        var mappedProduct = mapper.Map<ProductEntity>(product)!;
        var addedProduct = await repository.AddNewProduct(mappedProduct);
        var productResult = mapper.Map<ProductDto>(addedProduct);
        return TypedResults.Created($"api/products/{addedProduct.Id}", productResult);
    }

}
