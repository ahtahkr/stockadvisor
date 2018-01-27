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

            Companies companies = new Companies();
            companies.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
            companies.Get_Company_Robinhood();
            var company = new Companies { _companies = companies._companies, Database_Connection_String = companies.Database_Connection_String };

            return View(company);
        }
    }
}