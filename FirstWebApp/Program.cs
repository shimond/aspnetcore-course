using FirstWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HeaderRemoveConfig>(builder.Configuration.GetSection("HeaderToRemove"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Resolve DI
//builder.Services.AddSingleton<IProductsRepository, ProductsRepository2>();
//builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRemoveHeadersMiddleware();
app.MapControllers();

app.Run();

