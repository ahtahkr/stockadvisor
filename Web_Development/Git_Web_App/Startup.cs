using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AustinsFirstProject.Library;
using Microsoft.Data.Sqlite;
using System.IO;
using Newtonsoft.Json;

namespace AustinsFirstProject.Git_Web_App
{
    public class Startup
    {
        public Startup()
        {
            Library.Return _return = SQLiteDB.Database.Create_Database();

            if (_return.Result == 1)
            {
                Logger.Log_Error(_return.Message, "Startup");
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Index}/{action=Index}/{id?}");
                //routes.MapRoute("areaRoute", "{area:exists}/{controller=Company}/{action=Company}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
