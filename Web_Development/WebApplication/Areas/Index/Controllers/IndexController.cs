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
using Newtonsoft.Json;

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
            Shares shares = new Shares();
            shares._shares.Add(new Share("abcd" ));
            shares._shares.Add(new Share("cdef"));
            shares._shares.Add(new Share("asdfdf"));
            shares._shares.Add(new Share("65135"));
            shares._shares.Add(new Share("65131"));
            //var share = JsonConvert.SerializeObject(shares);
            var share = new Shares { _shares = shares._shares };
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