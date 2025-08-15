using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
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
app.UseCors("TodosPuedenEntrar");

// Instancia de ApiHelper
var apiHelper = new ApiHelper(app);

// Variables locales
int contador = 0;
string mensaje = "Inicial";
Dictionary<int, RobotInfo> robots = new()
{
    { 123456, new RobotInfo { X = 10.5, Y = 20.3, Grados = 90, Nombre = "Robot A" } },
    { 654321, new RobotInfo { X = 5.2, Y = 7.8, Grados = 180, Nombre = "Robot B" } }
};

// 1. API para devolver valores simples
apiHelper.GenerarAPI("/contador", () => contador);
apiHelper.GenerarAPI("/mensaje", () => mensaje);

// 2. API para devolver valores de un diccionario por ID
apiHelper.GenerarDiccionarioPorId("/robot/info", robots);

// 3. API para recibir datos y asignar a variables locales
apiHelper.GenerarAPIRecibir<string>("/mensaje", valor => mensaje = valor);
apiHelper.GenerarAPIRecibir<int>("/contador", valor => contador = valor);

// Simulación de incremento automático
_ = Task.Run(async () =>
{
    while (true)
    {
        contador++;
        await Task.Delay(1000);
    }
});

app.Run("http://localhost:5234");

// --- CLASES ---
public class RobotInfo
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Grados { get; set; }
    public string Nombre { get; set; } = "";
}
