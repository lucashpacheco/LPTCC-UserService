using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UserService.API.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
                                                                       IConfiguration configuration)
        {
            // IConfigurationSection appSettingsSection = configuration.GetSection("AppSettings");
            // services.Configure<TokenSettings>(appSettingsSection);

            byte[] key = Encoding.ASCII.GetBytes("12312903892013210938");

            services.
                AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = "Bearer";
                    x.DefaultChallengeScheme = "Bearer";
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)//,
                        //ValidateIssuer = true,
                        //ValidateAudience = true,
                        //ValidAudience = Configuracao.Audience,
                        //ValidIssuer = Configuracao.Issuer
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            return services;
        }
    }
}
