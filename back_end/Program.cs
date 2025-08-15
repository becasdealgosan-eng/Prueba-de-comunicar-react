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

// Crear diccionario (la clase se declara más abajo)
Dictionary<int, RobotInfo> robots = new()
{
    { 123456, new RobotInfo { X = 10.5, Y = 20.3, Grados = 90, Nombre = "Robot A" } },
    { 654321, new RobotInfo { X = 5.2, Y = 7.8, Grados = 180, Nombre = "Robot B" } }
};

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

// Endpoint que devuelve un robot por id
app.MapGet("/api/robot/info/{id:int}", (int id) =>
{
    if (robots.TryGetValue(id, out var info))
    {
        return Results.Json(info, new JsonSerializerOptions { WriteIndented = true });
    }
    return Results.NotFound(new { Error = "Robot no encontrado" });
});

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

// --- CLASES FUERA DEL BLOQUE PRINCIPAL ---
public class RobotInfo
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Grados { get; set; }
    public string Nombre { get; set; } = "";
}
