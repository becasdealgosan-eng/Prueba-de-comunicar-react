using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public class ApiHelper
{
    private readonly WebApplication _app;

    public ApiHelper(WebApplication app)
    {
        _app = app;
    }

    // 1. Generar un GET simple para devolver cualquier valor
    public void GenerarAPI(string ruta, Func<object> obtenerDatoActual)
    {
        _app.MapGet($"/api{ruta}", () =>
        {
            var datoActual = obtenerDatoActual();
            return Results.Json(datoActual, new JsonSerializerOptions { WriteIndented = true });
        });
    }

    // 2. Generar GET por ID para devolver valor de un diccionario
    public void GenerarDiccionarioPorId<T>(string ruta, Dictionary<int, T> diccionario)
    {
        _app.MapGet($"/api{ruta}/{{id:int}}", (int id) =>
        {
            if (diccionario.TryGetValue(id, out var valor))
            {
                return Results.Json(valor, new JsonSerializerOptions { WriteIndented = true });
            }
            return Results.NotFound(new { Error = "ID no encontrado" });
        });
    }

    // 3. Generar POST para recibir datos y asignarlos a una variable local
    public void GenerarAPIRecibir<T>(string ruta, Action<T> asignarVariable)
    {
        _app.MapPost($"/api{ruta}", async (HttpContext context) =>
        {
            var datoRecibido = await context.Request.ReadFromJsonAsync<T>();
            if (datoRecibido != null)
            {
                asignarVariable(datoRecibido); // Cambia la variable local
                return Results.Ok("SUCCESS");
            }
            return Results.BadRequest(new { Error = "Datos inválidos" });
        });
    }
}
