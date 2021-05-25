using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using XSchool.EntityFrameworkCore.DbContexts;

namespace XSchool.API.Extensions
{
    public static class AppExtensions
    {
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
