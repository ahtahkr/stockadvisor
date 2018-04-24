using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Stock_Advisor.Controllers
{
    [Route("[controller]")]
    public class VolumeAscendingController : Controller
    {
        private IConfigurationRoot configRoot;

        // GET: api/VolumeAscending
        [HttpGet]
        public IActionResult Get()
        {
            
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            ProductionDatabase.Modal.Volume_Ascending_Collection volume_Ascending_Collection = new ProductionDatabase.Modal.Volume_Ascending_Collection();
            volume_Ascending_Collection.Get_from_Database(connection_string, DateTime.Now);

            return View(volume_Ascending_Collection);
        }

        // GET: api/VolumeAscending/5
        [HttpGet("{dateTime}", Name = "Get")]
        public IActionResult Get(DateTime dateTime)
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            ProductionDatabase.Modal.Volume_Ascending_Collection volume_Ascending_Collection = new ProductionDatabase.Modal.Volume_Ascending_Collection();
            volume_Ascending_Collection.Get_from_Database(connection_string, dateTime);

            return View(volume_Ascending_Collection);
        }
        
        // POST: api/VolumeAscending
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/VolumeAscending/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
