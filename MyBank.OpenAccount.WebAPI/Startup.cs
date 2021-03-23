using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBank.Accounts.WebAPI.Client;
using MyBank.Domain;
using MyBank.Domain.Repositories;
using MyBank.Domain.Services;
using MyBank.OpenAccount.Infrastructure.EntityFrameworkCore;
using MyBank.OpenAccount.Infrastructure.EntityFrameworkCore.Repositories;
using System.Threading.Tasks;

namespace MyBank.OpenAccount.WebAPI
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
            services.AddControllersWithViews();
            services.AddDbContext<OpenAccountContext>()
                .AddTransient<IAccountClient, AccountClient>()
                .AddTransient<IAccountOpeningRequestRepository, AccountOpeningRequestRepository>()
                .AddTransient<IAdministrativeAccountOpeningService, AdministrativeAccountOpeningService>()
                .AddTransient<IAccountOpeningService, AccountOpeningService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<OpenAccountContext>())
                {
                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
            }
        }
    }
}
