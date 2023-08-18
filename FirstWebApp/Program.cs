var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.Use(async (context, next) => {
    await context.Response.WriteAsync(" MID 1 Start ");
    await next();
    await context.Response.WriteAsync(" MID 1 End   ");
});

app.Use(async (context, next) => {
    await context.Response.WriteAsync(" MID 2 Start ");
    await next();
    await context.Response.WriteAsync(" MID 2 End   ");
});


app.Run(async (context) => {
    await context.Response.WriteAsync(" Hello world!    ");
});

app.Run();
