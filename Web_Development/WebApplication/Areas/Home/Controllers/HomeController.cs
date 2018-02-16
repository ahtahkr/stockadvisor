using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;

namespace AustinsFirstProject.WebApplication.Areas.Home.Controllers
{
    [Area("Home")]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        private IConfigurationRoot configRoot;
        private Models.Home Home;

        [Route("")]
        public void Global_ReRoute()
        {
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            Response.Redirect(baseUrl + "/Home/Index");
        }

        [Route("[action]")]
        public ActionResult Index()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            this.Home = new Models.Home();
            this.Home.Connection_String = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);
            
            this.Home.Get_Symbols();
            return View(this.Home);
        }
    }
}