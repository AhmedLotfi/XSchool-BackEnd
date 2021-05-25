using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XSchool.EntityFrameworkCore.DbContexts;

namespace XSchool.API.Extensions
{
    public static class AppExtensions
    {
        public static void AddAppDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<XSchoolDbContext>(option => option.UseSqlServer(configuration["ConnectionStrings:SchoolConnectionString"]));
        }
    }
}
