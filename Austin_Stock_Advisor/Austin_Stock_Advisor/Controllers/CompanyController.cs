using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AustinStockAdvisor.Helper;
using System.IO;
using Newtonsoft.Json;

namespace AustinStockAdvisor.Controllers
{
    [Route("stock/[controller]")]
    public class CompanyController : Controller
    {
        private IConfigurationRoot configRoot;

        [HttpGet]
        public IActionResult Get()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Company_Get]"
                , connection_string);

            List<Library.Company> _companies = JsonConvert.DeserializeObject<List<Library.Company>>(companies);
            return View(_companies);
        }
    }
}
