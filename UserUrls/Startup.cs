using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataLayer.EF;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using DataLayer.Entities;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UserUrls
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<UrlDbContext>(
                options => options.UseSqlite(
                    "Data Source=Url.db",
                    x => x.MigrationsAssembly("DataLayer")));

            //you can use MockRepository instead of SQLiteRepository
            services.AddScoped<IRepository<UserUrl>, SQLiteRepository>();
            services.AddScoped<UrlService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var userUrlHandler = new RouteHandler(UserUrlHandler);
            var routeBuilder = new RouteBuilder(app, userUrlHandler);

            routeBuilder.MapRoute("User Route", "{*controller}");
            var userRoutes = routeBuilder.Build();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseRouter(userRoutes);
        }

        public async Task UserUrlHandler(HttpContext context)
        {
            var userUrl = context.GetRouteData().Values["controller"].ToString();
            string content = context.RequestServices.GetService<UrlService>()
                .GetContentByUrl(userUrl);

            if (content != "")
                await context.Response.WriteAsync(content);
            else {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("<h1>Not found</h1>");
            }
        }
    }
}
