using fp_stack.core.Models;
using fp_stack.core.Services;
using fp_stack.web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace fp_stack.web
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
            services.AddTransient<NoticiaService>();
            //services.AddScoped
            //services.AddSingleton

            var connection = @"Server=(localdb)\mssqllocaldb;Database=StackDB2;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));

            //services.AddDataProtection()
            //    .SetApplicationName("minhapp")
            //    .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"C:\testefiap"));

            services.AddMvc();

            services.AddAuthentication("app")
            .AddCookie("app",
            o =>
            {
                o.LoginPath = "/account/index";
                o.AccessDeniedPath = "/account/denied";

            });

            services.AddMemoryCache();

            services.Configure<GzipCompressionProviderOptions>(
            o => o.Level = System.IO.Compression.CompressionLevel.Fastest);
            services.AddResponseCompression(o =>
            {
                o.Providers.Add<GzipCompressionProvider>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMeuLog();


            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
