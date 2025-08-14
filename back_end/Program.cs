using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // o el puerto de tu React
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapGet("/api/lista", () =>
{
    var lista = new List<string>
    {
        "🍎 Manzana",
        "🍌 Plátano",
        "🍇 Uva",
        "🥭 Mango"
    };
    return Results.Json(lista);
});

app.Run();