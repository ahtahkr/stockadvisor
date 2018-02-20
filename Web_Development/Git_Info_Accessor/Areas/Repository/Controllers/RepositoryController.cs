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
        public RepositoryController()
        {
        }

        [Route("")]
        public void ReRoute()
        {
            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            Response.Redirect(baseUrl + "/Repository/Index/github/fetch");
        }

        [Route("[action]/{owner}/{repo}")]
        public IActionResult Index(string owner, string repo)
        {
            if (String.IsNullOrEmpty(owner) || String.IsNullOrEmpty(repo))
            {
                string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                Response.Redirect(baseUrl);
            }
            string result = Github_Api.Api.Rest_Api_V3.Repositories.Get_Basic_Info(owner, repo);

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
            repository.Api_String = result;

            return View(repository);
        }

        [Route("[action]/{owner}/{repo}")]
        public IActionResult Commits(string owner, string repo)
        {
            if (String.IsNullOrEmpty(owner) || String.IsNullOrEmpty(repo))
            {
                string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                Response.Redirect(baseUrl);
            }
            string result = Github_Api.Api.Rest_Api_V3.Repositories.Get_Commits(owner, repo);

            List<Github_Api.Model.CommitEvent> commit_event_list = new List<Github_Api.Model.CommitEvent>();

            if (String.IsNullOrEmpty(result)) { }
            else
            {
                try
                {
                    commit_event_list = JsonConvert.DeserializeObject<List<Github_Api.Model.CommitEvent>>(result);
                }
                catch (Exception ex)
                {
                    commit_event_list = new List<Github_Api.Model.CommitEvent>();
                }
            }
            return View(commit_event_list);
        }
    }
}