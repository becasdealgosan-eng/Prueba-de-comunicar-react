using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// Permitir cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("TodosPuedenEntrar", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Activar la política antes de mapear rutas
app.UseCors("TodosPuedenEntrar");

// Función para registrar un endpoint dinámico
void GenerarAPI(string ruta, Func<object> obtenerDatoActual)
{
    app.MapGet($"/api{ruta}", (HttpContext context) =>
    {
        var datoActual = obtenerDatoActual(); // Obtiene el valor en el momento de la petición
        return Results.Json(datoActual, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    });
}

// Ejemplo de variable que puede cambiar
int contador = 0;

// Generamos una API que devuelve el valor actual de contador
GenerarAPI("/robot/posicion", () => contador);

// Ejemplo: cada segundo cambia la variable (simulación)
_ = Task.Run(async () =>
{
    while (true)
    {
        contador++;
        await Task.Delay(1000);
    }
});

app.Run("http://localhost:5234");