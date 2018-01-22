using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;

namespace WebApplication.Areas.Index.Controllers
{
    [Area("Index")]
    [Route("index")]
    public class IndexController : Controller
    {
        private IConfigurationRoot configRoot;

        public string Index()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            return configRoot.GetConnectionString("DefaultConnection");
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