using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.StockAdvisor.IEXTrading;
using Microsoft.Extensions.Configuration;
using AustinsFirstProject.StockAdvisor.WebApplication.Helper;
using System.IO;
using Newtonsoft.Json;


namespace AustinsFirstProject.WebApplication.Areas.Robinhood.Controllers
{
    [Area("Robinhood")]
    [Route("Robinhood")]
    public class RobinhoodController : Controller
    {
        private IConfigurationRoot configRoot;

        [Route("")]
        public void ReRoute()
        {
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            Response.Redirect(baseUrl + "/Robinhood/Index");
        }

        [Route("[action]/{closemin:int?}/{closemax:int?}/{page:int?}")]
        public IActionResult Index(int closemin = 0, int closemax = 2, int page = 1)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CloseMax", closemax },
                { "CloseMin", closemin },
                { "Page", page }
            };

            configRoot = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

            string result = AustinsFirstProject.Library.Database.ExecuteProcedure_Get("[webApp].[Get_ShareDetail_Close]", param, configRoot.GetConnectionString(configRoot.GetSection("environmentVariables")["ENVIRONMENT"]));

            List<Models.ShareDetail> sharedetail = JsonConvert.DeserializeObject<List<Models.ShareDetail>>(result);

            Models.ShareCanvas shareCanvas = new Models.ShareCanvas();
            shareCanvas.Set_ShareDetail(sharedetail);

            

            return View(shareCanvas);
        }
    }
}