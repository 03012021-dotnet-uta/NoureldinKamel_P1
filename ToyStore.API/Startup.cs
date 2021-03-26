using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ToyStore.Business.Logic;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;

namespace ToyStore.API
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
            // todo: ask how to fix the import here
            string connectionString = Configuration.GetConnectionString("ToysDb");
            services.AddDbContext<DbContextClass>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // todo: add scoped stuff here
            services.AddScoped<SellableLogic>();
            services.AddScoped<ToyRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToyStore.API", Version = "v1" });
            });
            // services.AddControllersWithViews()
            //     .AddNewtonsoftJson(options =>
            //     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToyStore.API v1"));
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            // app.UseRewriter(new Microsoft.AspNetCore.Rewrite.RewriteOptions())

            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^$", "toydetail/toydetail.html"));

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
