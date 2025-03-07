
using FirstWebApp.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(ProductsProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AspnetCourseFromDbContext>(o => o.UseInMemoryDatabase("coursesDatabase"));

builder.Services.AddCors(action => action.AddPolicy("aspnet-course",
    config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

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
