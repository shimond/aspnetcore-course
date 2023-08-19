using FirstWebApp.Models.Config;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.Configure<HeaderRemoveConfig>(builder.Configuration.GetSection("HeaderToRemove"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) => {
	try
	{
        await next();
    }
	catch (Exception)
	{

		throw;
	}
});

app.UseRemoveHeadersMiddleware();

app.Run(async (context) =>
{
	var valueFromConfig = app.Configuration["Logging:LogLevel:Default"];
	await context.Response.WriteAsync(valueFromConfig);
});

app.Run();
