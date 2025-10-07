var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();


app.MapGet("/empty", () =>
{
    return "Empty project";
});

app.Run();
