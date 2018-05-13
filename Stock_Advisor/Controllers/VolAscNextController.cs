using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Stock_Advisor.Controllers
{
    [Route("[controller]")]
    public class VolAscNextController : Controller
    {
        private IConfigurationRoot configRoot;

        [HttpGet]
        public IActionResult Get()
        {

            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            ProductionDatabase.Modal.Vol_Asc_Next_Collection volume_Ascending_Collection = new ProductionDatabase.Modal.Vol_Asc_Next_Collection();
            volume_Ascending_Collection.Get_from_Database(connection_string, DateTime.Now);

            return View(volume_Ascending_Collection);
        }

        // GET: api/VolAscNext/2018-05-13
        [HttpGet("{dateTime}", Name = "GetVolAscNext")]
        public IActionResult Get(DateTime dateTime)
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            ProductionDatabase.Modal.Vol_Asc_Next_Collection volume_Ascending_Collection = new ProductionDatabase.Modal.Vol_Asc_Next_Collection();
            volume_Ascending_Collection.Get_from_Database(connection_string, dateTime);

            return View(volume_Ascending_Collection);
        }
    }
}
