using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using rcCryptography;
using System;
using System.Text;

namespace rcAuthApi
{
    public class Authentication
    {
        public static void SetAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            string myHashKey = Crypto.GetKeyMD5(configuration.GetValue<string>("AuthKey"));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(myHashKey));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidIssuer = configuration.GetValue<string>("AuthIssuer"),
                    ValidAudience = configuration.GetValue<string>("AuthAudience")
                };
            });
        }
    }
}
