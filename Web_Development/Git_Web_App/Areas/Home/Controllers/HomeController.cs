using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.Library;
using Newtonsoft.Json;
using AustinsFirstProject.Git_Web_App.Model;

namespace AustinsFirstProject.Git_Web_App.Areas.Home.Controllers
{
    [Area("Home")]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {

        [Route("")]
        public IActionResult Index()
        {
            Models.Home home = new Models.Home();
            return View(home);
        }

        [Route("[action]/{git}")]
        public IActionResult Detail(string git_url)
        {
            Git_Url_Basic_Info git_object = new Git_Url_Basic_Info("", git_url);

            return View(git_object);
        }
    }
}