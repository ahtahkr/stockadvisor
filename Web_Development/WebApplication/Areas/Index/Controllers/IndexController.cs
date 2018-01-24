using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;
using AustinsFirstProject.Library;
using AustinsFirstProject.CoreLibrary.Database;

namespace WebApplication.Areas.Index.Controllers
{
    [Area("Index")]
    [Route("index")]
    public class IndexController : Controller
    {
        private IConfigurationRoot configRoot;

        public IActionResult Index()
        {
            //configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            //return configRoot.GetConnectionString("DefaultConnection");
            var share = new Share { Ticker = "abcd" };
            return View(share);
        }

        [Route("[action]/{page:int?}")]
        public string Orders()
        {
            return "Orders";
        }

        [Route("[action]")]
        public string Shop()
        {
            return "Shop";
        }

        [Route("[action]/newest")]
        public string Payments()
        {
            return "Payments";
        }
    }
}