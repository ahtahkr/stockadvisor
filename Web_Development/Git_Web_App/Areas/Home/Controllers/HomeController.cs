using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.Library;
using Newtonsoft.Json;
using AustinsFirstProject.Git_Web_App.Model;
using AustinsFirstProject.Git_Web_App.Classes;

namespace AustinsFirstProject.Git_Web_App.Areas.Home.Controllers
{
    [Area("Home")]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        private Models.Home Home;
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
        public string Committers(string git_url)
        {
            //Git_Url_Basic_Info git_object = new Git_Url_Basic_Info("", git_url);

            return "Committers for : " + git_url;
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