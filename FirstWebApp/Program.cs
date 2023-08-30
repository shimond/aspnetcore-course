var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HeaderRemoveConfig>(builder.Configuration.GetSection("HeaderToRemove"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CourseDbContext>(o => o.UseInMemoryDatabase("CourseDb"));
builder.Services.AddCors(action => action.AddPolicy("aspnet-course",
    config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//Resolve DI
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

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
