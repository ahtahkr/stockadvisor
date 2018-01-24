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
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            
            Shares shares = new Shares();
            shares.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
            shares.Get_All_Shares();
            shares.Identity = "Shares";
            var share = new Shares { _shares = shares._shares };
            return View(share);
        }

        [Route("[action]")]
        [Route("[action]/{ticker}")]
        public IActionResult Ticker(string ticker)
        {
            // /index/ticker
            // /index/ticker/5
            

            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            Shares shares = new Shares();
            shares.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
            shares.Get_Ticker(ticker);
            shares.Identity = ticker;

            var share = shares;

            return View(share);
        }

        [Route("[action]/{page:int?}")]
        public string Orders()
        {
            // /index/orders
            // /index/orders/5
            return "Orders";
        }

        [Route("[action]")]
        public string Shop()
        {
            // /index/shop
            return "Shop";
        }

        [Route("[action]/newest")]
        public string Payments()
        {
            return "Payments";
        }
    }
}