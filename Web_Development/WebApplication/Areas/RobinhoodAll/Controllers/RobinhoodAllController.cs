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
        private Companies companies;

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{page:int?}")]
        [Route("[action]/{open:int?}/{page:int?}")]
        public IActionResult Index(int open, int page)
        {
            if (open > 0) { } else { open = 200; }

            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            companies = new Companies();
            companies.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
            companies.Get_Company_Robinhood(open);

            Companies _company = new Companies();
            //string test = "";
            for (int a = (page * 5); a <= ((page*5)+4); a++)
            {
                
                if ( this.companies._companies.Count > a)
                {
                    //test += a + ", ";
                    _company._companies.Add(this.companies._companies[a]);
                }
            }
            //test += "[" + this.companies._companies.Count + "]";
            var company = new Companies { _companies = _company._companies, Database_Connection_String = companies.Database_Connection_String };
            //return test;
            return View(company);
        }
    }
}