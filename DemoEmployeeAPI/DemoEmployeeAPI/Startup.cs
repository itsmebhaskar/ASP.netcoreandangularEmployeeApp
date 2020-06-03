using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoEmployeeAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoEmployeeAPI
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
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("p1", confPolicy =>
                 {
                     confPolicy.AllowAnyOrigin().WithOrigins(new string[] { "http://localhost:4200" }).WithMethods("GET", "DELETE", "POST").AllowAnyHeader();
                 });
            });


            var conn = Configuration.GetConnectionString("EmployeeDb");

            services.AddDbContext<EmployeeContext>(optionsBuilder=>
                            optionsBuilder.UseSqlServer(conn));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           

            app.UseRouting();

            app.UseCors("p1");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
