using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagement.Application.Common;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Auth;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Infrastructure.Persistence.Repositories;

namespace TaskManagement.Infrastructure
{
    /// <summary>
    /// Dependency injection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add infrastructure
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="config">Configuration</param>
        /// <returns>Services</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Register all infrastructure-level services 
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUserContext, UserContext>();

            // JWT configuration
            var jwtSettings = config.GetSection("Jwt");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = config["JWT:ValidAudience"],
                    ValidIssuer = config["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]))
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
