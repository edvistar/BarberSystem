using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System.Text;

namespace API.Extensions
{
    public static class ServiceIdentityExtension
    {
        public static IServiceCollection AgregarServiceIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<UsuarioAplicacion>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<RolAplicacion>()
                .AddRoleManager<RoleManager<RolAplicacion>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminRol", policy => policy.RequireRole("Admin"));
            });
            return services;    
        }
    }
}
