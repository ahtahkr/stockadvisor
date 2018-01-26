using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using Microsoft.Extensions.Configuration;
using System.IO;
using AustinsFirstProject.CoreLibrary.Database;
using AustinsFirstProject.Library;
using Newtonsoft.Json;

namespace WebApplication.Areas.Company
{
    public class CompanyController : Controller
    {
        private IConfigurationRoot configRoot;
        public IActionResult Index()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            Companies companies = new Companies();
            companies.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
            companies.Get_Company_Filed();
            var company = new Companies { _companies = companies._companies };

            return View(company);
        }

        public ActionResult GetMessage()
        {
            Logger.Log("Click", "Click", "Click");
            string message = "Welcome";
            return new JsonResult(message);
        }
        
        public int Update_Robinhood(string company)
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            
            AustinsFirstProject.CoreLibrary.Database.Company _company
                = JsonConvert.DeserializeObject<AustinsFirstProject.CoreLibrary.Database.Company>(company);
            _company.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");

            return _company.Update_Robinhood();
        }
    }
}