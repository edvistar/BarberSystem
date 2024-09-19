using API.Extensions;
using API.Hubs;
using API.Middleware;
using Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AgregarServiceApplication(builder.Configuration);

builder.Services.AgregarServiceIdentity(builder.Configuration);
builder.Services.AddScoped<IDbInicializador, DbInicializador>();

var app = builder.Build();
// Configura el middleware de SignalR
app.MapHub<OrdenHub>("/ordenHub");

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errores/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
                  
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var inicializador = services.GetRequiredService<IDbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception ex)
    {

        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrio un error al ejecutar la migracion");
    }
}
app.MapControllers();

app.Run();
