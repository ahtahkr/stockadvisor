using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;

namespace AustinsFirstProject.WebApplication.Areas.Symbol.Controllers
{
    [Area("Symbol")]
    [Route("")]
    [Route("Symbol")]
    public class SymbolController : Controller
    {
        private IConfigurationRoot configRoot;
        private Models.Symbol Symbol;

        [Route("")]
        public void Global_ReRoute()
        {
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            Response.Redirect(baseUrl + "/Symbol/Index");
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            this.Symbol = new Models.Symbol();
            this.Symbol.Connection_String = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            this.Symbol.Get_Symbols();
            return View(this.Symbol);
        }
    }
}