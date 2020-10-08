using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LittleDebugger.ApiWithClientTemplate.Service.Models;
using System;

namespace LittleDebugger.ApiWithClientTemplate.Service
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
            services
                .AddControllers()
                .AddNewtonsoftJson();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "PRODUCTION")
            {
                services.AddDbContext<ApiWithClientTemplateDatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ApiWithClientTemplateConnectionString")));
            }
            else
            {
                services.AddDbContext<ApiWithClientTemplateDatabaseContext>(options =>
                    options.UseSqlServer("Server = tcp:ApiWithClientTemplate.database.windows.net,1433; Database = ApiWithClientTemplateDb; User ID = dbAdmin; Password = fdajkngoan43k!; Encrypt = true; Connection Timeout = 30;"));
                
                //services.AddDbContext<ApiWithClientTemplateDatabaseContext>(options =>
                //  options.UseSqlite("Data Source=localdatabase.db"));
            }

            services.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Market Data Service";
                    document.Info.Description = "Service for Managing Market data.";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApiWithClientTemplateDatabaseContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}

