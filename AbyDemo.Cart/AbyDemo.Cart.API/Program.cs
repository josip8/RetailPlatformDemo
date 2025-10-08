using AbyDemo.Cart.API.Filters;
using AbyDemo.Cart.Application.Config;
using AbyDemo.Cart.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddMongoDatabase(builder.Configuration);
builder.Services.AddProductService();
builder.Services.AddEventPublisher();
builder.Services.AddApplicationServices();
builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
