using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.Library;
using Newtonsoft.Json;
using AustinsFirstProject.Git_Web_App.Model;
using AustinsFirstProject.Git_Web_App.Classes;
using LibGit2Sharp;
using Newtonsoft.Json.Linq;

namespace AustinsFirstProject.Git_Web_App.Areas.Home.Controllers
{
    [Area("Home")]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        private Models.Home Home = null;
        public HomeController()
        {
            this.Home = new Models.Home();
        }

        [Route("")]
        public IActionResult Index()
        {
            Models.Home home = new Models.Home();
            return View(this.Home);
        }

        [Route("[action]")]
        public IActionResult Detail(string git_url)
        {
            Git_Url_Basic_Info git_object = new Git_Url_Basic_Info("", git_url);
            
            return View(git_object);
        }

        [Route("[action]")]
        public IActionResult Committers(string git_url)
        {
            Logger.Log(git_url, "Committers");
            Committers committers = new Committers(git_url);
            return View(committers);
        }

        [Route("[action]")]
        public string Committer_Selected(string _input)
        {
            var input = JObject.Parse(_input);

            string email = (string)input["email"];
            string repo_url = (string)input["repo_url"];

            string org = repo_url;

            if (repo_url.ElementAt(0) == '"')
            {
                repo_url = repo_url.Substring(1, repo_url.Length - 1);
            }

            if (repo_url.ElementAt(repo_url.Length - 1) == '"')
            {
                repo_url = repo_url.Substring(0, repo_url.Length - 1);
            }

            if (Repository.IsValid(repo_url))
            {
                return "Valid. " + repo_url + " : " + org;
            } else
            {
                return "Invalid. " + repo_url + " : " + org;
            }

//            return email + " : " + repo_url;
        }

        [Route("[action]")]
        public string Get_Active_Git_Commit(string commit)
        {
            Git_Commit git_commit = JsonConvert.DeserializeObject<Git_Commit>(commit);
            if (git_commit.Set_Files_and_Status())
            {
                return JsonConvert.SerializeObject(git_commit);
            } else
            {
                return JsonConvert.SerializeObject(new Git_Commit());
            }
        }
    }
}