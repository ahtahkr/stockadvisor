using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using AustinStockAdvisor.Helper;

namespace Austin_Stock_Advisor.Controllers
{
    [Route("stock/[controller]")]
    public class ShareController : Controller
    {
        private IConfigurationRoot configRoot;

        [HttpGet]
        public string Get()
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("symbol", "aapl");

            string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Get]"
                , connection_string, param);

            return companies;
        }

        // GET: api/Share/5
        [HttpGet("{symbol}", Name = "ShareGet")]
        public string Get(string symbol)
        {
            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            string connection_string = configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("symbol", symbol);

            string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Get]"
                , connection_string, param);

            return companies;
        }
        /*
        // POST: api/Share
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Share/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
