using FirstWebApp.EndPoints;

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

app.MapProducts();

app.Run();
