﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockAdvisor.WebApplication.Helper;
using Library;
using CoreLibrary.Database;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Areas.Company.Controllers
{
    [Area("Company")]
    [Route("Company")]
    public class CompanyController : Controller
    {
        private IConfigurationRoot configRoot;

        [Route("")]
        public IActionResult Index()
        {
            Models.Company company = new Models.Company();
            try
            {
                configRoot = ConfigurationHelper.GetConfiguration();

                Companies companies = new Companies();
                companies.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");
                companies.Get_Company_Filed();

                company.companies = companies;

            } catch (Exception ex)
            {
                Logger.Log_Error("CompanyController Index failed. Message: " + ex.Message);
            }
            return View(company);
        }

        [Route("[action]")]
        public int Update_Robinhood(string company)
        {
            configRoot = ConfigurationHelper.GetConfiguration();
            CoreLibrary.Database.Company _company
                    = JsonConvert.DeserializeObject<CoreLibrary.Database.Company>(company);
            _company.Database_Connection_String = configRoot.GetConnectionString("DefaultConnection");

            return _company.Update_Robinhood();
        }
    }
}