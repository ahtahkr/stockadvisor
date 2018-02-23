using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;
using AustinsFirstProject.CoreLibrary.Database;
using AustinsFirstProject.Library;
using Newtonsoft.Json;

namespace AustinsFirstProject.WebApplication.Areas.RobinhoodAll.Controllers
{
    [Area("RobinhoodAll")]
    [Route("RobinhoodAll")]
    public class RobinhoodAllController : Controller
    {
        private IConfigurationRoot configRoot;
        private Companies companies;

        //[Route("")]
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [Route("")]
        [Route("{page:int?}")]
        [Route("[action]/{open:int?}/{page:int?}")]
        public IActionResult Index(int open, int page)
        {
            Models.RobinhoodAll _robinhoodAll = new Models.RobinhoodAll();
            try
            {
                if (open > 0) { } else { open = 200; }

                configRoot = ConfigurationHelper.GetConfiguration();

                companies = new Companies();
                companies.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
                companies.Get_Company_Robinhood(open);

                Models.RobinhoodAll robinhoodAll = new Models.RobinhoodAll();

                Companies _company = new Companies();
                for (int a = (page * 5); a <= ((page * 5) + 4); a++)
                {
                    if (this.companies._companies.Count > a)
                    {
                        //test += a + ", ";
                        _company._companies.Add(this.companies._companies[a]);
                    }
                }
                _company.Database_Connection_String = companies.Database_Connection_String;

                _robinhoodAll._Companies = _company;

                double pages = (double)this.companies._companies.Count / 5;
                int _pages = (int)pages;

                if (_pages < pages)
                {
                    _robinhoodAll.Pages = _pages + 1;
                }
                else
                {
                    _robinhoodAll.Pages = _pages;
                }
                _robinhoodAll.Open = open;
            } catch (Exception ex)
            {
                Logger.Log_Error("RobinhoodController Index failed. Message: " + ex.Message);
            }

            return View(_robinhoodAll);
        }
    }
}