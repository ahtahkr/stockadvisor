using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AustinsFirstProject.Git_Info_Accessor.Areas.Repository.Controllers
{
    [Area("Repository")]
    [Route("")]
    [Route("Repository")]
    public class RepositoryController : Controller
    {
        [Route("")]
        public void ReRoute()
        {
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            Response.Redirect(baseUrl + "/Repository/Index");
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            string result = Github_Api.Api.Rest_Api_V3.Repositories.Get_Basic_Info("bwillis", "jekyll-github-sample");

            Github_Api.Model.Repository repository = new Github_Api.Model.Repository();
            if (String.IsNullOrEmpty(result)) { }
            else
            {
                try
                {
                    repository = JsonConvert.DeserializeObject<Github_Api.Model.Repository>(result);
                }
                catch (Exception ex)
                {
                    repository = new Github_Api.Model.Repository();
                }
            }

            return View(repository);
        }
    }
}