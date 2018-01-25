using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using Microsoft.Extensions.Configuration;
using System.IO;
using AustinsFirstProject.CoreLibrary.Database;

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
    }
}