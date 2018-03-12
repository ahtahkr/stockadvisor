using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Austin_Stock_Advisor.Controllers
{
    [Route("stock/[controller]")]
    public class ChangeController : Controller
    {
        // GET: api/Change
        [HttpGet]
        public string Get()
        {
            return 2 + ":" + 2 + ":" + 0;
        }

        // GET: api/Change/5
        [HttpGet("{WeeksToGoBack}/{MaxHigh}/{MinChange}", Name = "Get")]
        public string Get(int WeeksToGoBack, int MaxHigh, int MinChange)
        {
            return WeeksToGoBack + ":" + MaxHigh + ":" + MinChange;
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
