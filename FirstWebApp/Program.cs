using FirstWebApp.Contracts;
using FirstWebApp.Models.DataEntities;
using FirstWebApp.Services;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HeaderRemoveConfig>(builder.Configuration.GetSection("HeaderToRemove"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(action => action.AddPolicy("aspnet-course",
    config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//Resolve DI
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("aspnet-course");

app.UseHttpsRedirection();

app.MapGet("time", () => DateTime.Now).WithTags("Time");

app.MapGet("test/{id}", (int id) => $"The value is {id}").WithTags("Tests");

app.MapGet("testIt/{name}/{id}", (string name, int id, string? description) => $"The value is {name} {description} {id}").WithTags("Tests");

var productsGroup = app.MapGroup("products").WithTags("Products").RequireAuthorization();

productsGroup.MapGet("", async Task<Ok<List<Product>>> (IProductsRepository repository) =>
{
    var result = await repository.GetAllProducts();
    return TypedResults.Ok(result);
});

productsGroup.MapGet("{id}", async Task<Results<NotFound, Ok<Product>>> (int id, IProductsRepository repository) =>
{
    var result = await repository.GetProductById(id);
    if (result == null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
});

productsGroup.MapDelete("{id}", async Task<Results<NotFound, Ok<Product>>> (int id, IProductsRepository repository) =>
{
    var result = await repository.GetProductById(id);
    if (result == null)
    {
        return TypedResults.NotFound();
    }

    await repository.DeleteItem(id);

    return TypedResults.Ok(result);
});

productsGroup.MapPut("{id}", async Task<Results<NotFound, Ok<Product>>> (int id, Product product, IProductsRepository repository) =>
{
    var result = await repository.GetProductById(id);
    if (result == null)
    {
        return TypedResults.NotFound();
    }

    var productAfterUpdate = await repository.UpdateProduct(id, product);
    return TypedResults.Ok(productAfterUpdate);
});

productsGroup.MapPost("", async Task<Created<Product>> (Product product, IProductsRepository repository) =>
{
    var addedProduct = await repository.AddNewProduct(product);
    return  TypedResults.Created($"api/products/{addedProduct.Id}", addedProduct);
}); 


app.Run();
