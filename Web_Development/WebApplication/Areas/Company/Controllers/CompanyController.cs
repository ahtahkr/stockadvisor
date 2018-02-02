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

namespace AustinsFirstProject.WebApplication.Areas.Index.Controllers
{
    [Area("Company")]
    [Route("Company")]
    public class CompanyController : Controller
    {
        private IConfigurationRoot configRoot;

        [Route("")]
        public IActionResult Index()
        {
            var company = new Companies();
            try
            {
                configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

                Companies companies = new Companies();
                companies.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
                companies.Get_Company_Filed();
                company = new Companies { _companies = companies._companies };
            } catch (Exception ex)
            {
                Logger.Log_Error("CompanyController Index failed. Message: " + ex.Message);
            }

            Logger.Log_Error(JsonConvert.SerializeObject(company));
            return View(company);
        }

        //public ActionResult GetMessage()
        //{
        //    string message = "Welcome";
        //    return new JsonResult(message);
        //}
        
        [Route("[action]")]
        public int Update_Robinhood(string company)
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            AustinsFirstProject.CoreLibrary.Database.Company _company
                = JsonConvert.DeserializeObject<AustinsFirstProject.CoreLibrary.Database.Company>(company);
            _company.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");

            return _company.Update_Robinhood();
        }

        [Route("[action]")]
        [Route("[action]/{ticker}")]
        public string Ticker()
        {

            return "Company Tickers";
        }

        [Route("[action]/{page:int?}")]
        public string Orders()
        {
            // /index/orders
            // /index/orders/5
            return "Company Orders";
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
            return "Company Payments";
        }
    }
}