using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using XSchool.Domain.Core.ErrorModels;
using XSchool.EntityFrameworkCore.DbContexts;

namespace XSchool.API.Extensions
{
    public static class AppExtensions
    {
        public static void ConfigureAPIBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(z => z.Value.Errors.Count > 0)
                    .SelectMany(z => z.Value.Errors)
                    .Select(z => z.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        public static void AddAppDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<XSchoolDbContext>(option => option.UseSqlServer(configuration["ConnectionStrings:SchoolConnectionString"]));
        }

        public static void AddAppJWTAuthenticaionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = configuration["Tokens:Issuer"],
                     ValidAudience = configuration["Tokens:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),
                     ClockSkew = TimeSpan.Zero,
                 };
             });
        }
    }
}
