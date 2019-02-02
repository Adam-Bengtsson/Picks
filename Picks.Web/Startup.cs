using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Picks.Infrastructure;
using Picks.Infrastructure.DataAccess;
using Picks.Infrastructure.Repositories;

namespace Picks.Web
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration conf)
        {
            _configuration = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = _configuration.GetConnectionString("Picks");

            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn, x => x.MigrationsAssembly("Picks.Web")));

            services.AddTransient<IPictureRepository, PictureRepository>();

            services.Configure<CustomAppSettings>(_configuration.GetSection("CustomAppSettings"));

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            //app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Browse}/{action=Browse}"
                );
            });
        }
    }
}
