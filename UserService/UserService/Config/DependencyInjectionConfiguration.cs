using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Repository;
using UserService.Repository.Context;
using UserService.Service;

namespace UserService.API.Config
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services,
                                                                                  IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("cnSql"), b => b.MigrationsAssembly("UserService.API")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                  .AddEntityFrameworkStores<IdentityContext>()
                  .AddDefaultTokenProviders();

            services.AddScoped<ICommandHandler, CommandHandler>();

            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<UserManager<IdentityUser>>();
            // OpenApiInfo info = new OpenApiInfo()
            // {
            //     Title = "MicroServiço Core - Gomed",
            //     Description = "Serviços disponíveis para aceeso ao sistema Gomed.",
            //     Contact = new OpenApiContact() { Name = "Suporte", Url = new Uri("http://www.gomed.med.br"), Email = "suporte@gomed.med.br" }
            // };

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(s => new ConfigureSwaggerOptions(services, info));

            return services;
        }
    }
}
