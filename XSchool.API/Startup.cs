using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using XSchool.API.Extensions;
using XSchool.Domain.Core.Middlewares;
using XSchool.Services.App.Users;

namespace XSchool.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAppDbContextService(Configuration);
            services.AddAppJWTAuthenticaionService(Configuration);
            services.AddSwaggerDocumentation();

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(UsersAppService)))
            .Where(c => c.Name.EndsWith("AppService"))
            .AsPublicImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseMiddleware<ExceptionMiddleware>();

            _ = app.UseStatusCodePagesWithReExecute("/errors/{0}");

            _ = app.UseHttpsRedirection();

            _ = app.UseStaticFiles();

            _ = app.UseCors("CorsPolicy");

            _ = app.UseAuthentication();

            _ = app.UseAuthorization();

            _ = app.UseSwaggerDocumention();

            _ = app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
