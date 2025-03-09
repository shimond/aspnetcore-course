
using FirstWebApp.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(ProductsProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AspnetCourseFromDbContext>(o => o.UseInMemoryDatabase("coursesDatabase"));

builder.Services.AddCors(action => action.AddPolicy("aspnet-course",
    config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddAntiforgery();

builder.Services.AddKeyedScoped<IPaymentProcessor, StripePaymentProcessor>("stripe");
builder.Services.AddKeyedScoped<IPaymentProcessor, PaypalPaymentProcessor>("paypal");

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(options =>
{
    options.CultureInfoUseUserOverride = false;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("aspnet-course");
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapProducts();
app.MapPayments();
app.Run();
