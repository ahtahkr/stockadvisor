using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;
using AustinsFirstProject.CoreLibrary.Database;

namespace WebApplication.Areas.RobinhoodAll.Controllers
{
    [Area("RobinhoodAll")]
    [Route("RobinhoodAll")]
    public class RobinhoodAllController : Controller
    {
        private IConfigurationRoot configRoot;

        [Route("")]
        public IActionResult Index()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            Shares shares = new Shares();
            shares.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
            shares.Get_Share_Robinhood();
            shares.Identity = "Shares";
            var share = new Shares { _shares = shares._shares };

            return View(share);
        }
    }
}