using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.BusinessService;
using BugTracker.BusinessService.Interface;
using BugTracker.DataService;
using BugTracker.DataService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BugTracker.API
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
            services.AddHttpContextAccessor();

            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            services.AddTransient<IDbConnectionCreator>(x => new MsSqlDbConnectionCreator(builder.ConnectionString));

            services.AddTransient<IUserDataService, UserDataService>();
            services.AddTransient<IBugDataService, BugDataService>();
            services.AddTransient<IWorkFlowDataService, WorkFlowDataService>();

            services.AddTransient<IBugBusinessService, BugBusinessService>();
            services.AddTransient<IUserBusinessService, UserBusinessService>();
            services.AddTransient<IWorkFlowBusinessService, WorkFlowBusinessService>();
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("You've reached Bug Tracker");
                });
            });
        }
    }
}
