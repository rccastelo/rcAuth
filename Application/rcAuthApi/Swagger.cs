using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace rcAuthApi
{
    public class Swagger
    {
        public static void SetSwagger(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Title = configuration.GetValue<string>("SystemTitle"),
                    Description = configuration.GetValue<string>("SystemDescription"),
                    Version = "1.0"
                });

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme {
                    Description = "Autenticação utilizando Bearer. Exemplo: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "rc-scheme",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement { {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme }
                        },
                        new[] { "readAccess", "writeAccess" }
                } });

                options.EnableAnnotations();
            });
        }
    }
}
