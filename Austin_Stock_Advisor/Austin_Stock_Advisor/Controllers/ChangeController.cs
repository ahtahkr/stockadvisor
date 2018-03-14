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
    public class ChangeController : Controller
    {
        private IConfigurationRoot configRoot;

        // GET: api/Change
        [HttpGet]
        public IActionResult Get()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Change]"
                , connection_string);

            List<Library.Change> changes = JsonConvert.DeserializeObject<List<Library.Change>>(companies);

            return View(changes);
        }

        // GET: stock/Change/3/4/5
        [HttpGet("{WeeksToGoBack}/{MaxHigh}/{MinChange}/{Avg_Volume}", Name = "Get")]
        public IActionResult Get(int WeeksToGoBack, int MaxHigh, int MinChange, int Avg_Volume)
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("WeeksToGoBack", WeeksToGoBack);
            param.Add("Max_High", MaxHigh);
            param.Add("Minimum_Change", MinChange);
            param.Add("Avg_Volume", Avg_Volume);

            string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Change]"
                , connection_string
                , param);

            List<Library.Change> changes = JsonConvert.DeserializeObject<List<Library.Change>>(companies);

            return View(changes);
        }
        
        // POST: api/Change
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Change/5
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
