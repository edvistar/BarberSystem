using Data.Interfaces;
using Data.Services;
using Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Errores;
using Utilities;
using Data.Interfaces.IRepository;
using Data.Repository;
using BLL.Services.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class ServiceApplicationExtension
    {
        public static IServiceCollection AgregarServiceApplication(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingresar Bearer [espacio] token \r\n\r\n " +
                                    "Ejemplo: Bearer ejpojhjhyg6465656",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oaut2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
            });
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("https://kairos.softallweb.co", "http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials(); // Permite credenciales con origen específico
                });
               
            });
            
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errores = actionContext.ModelState
                                  .Where(e => e.Value.Errors.Count > 0)
                                  .SelectMany(x => x.Value.Errors)
                                  .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errores = errores
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddScoped<IUnitWork, UnitWork>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IChairService, ChairService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IOrdenService, OrdenService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            // Agregar SignalR
            services.AddSignalR();
            return services;
        }
    }
}
