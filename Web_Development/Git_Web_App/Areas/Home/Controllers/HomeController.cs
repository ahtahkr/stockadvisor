using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AustinsFirstProject.Git_Web_App.Areas.Home.Controllers
{
    [Area("Home")]
    [Route("Home")]
    public class HomeController : Controller
    {

        [Route("")]
        public IActionResult Index()
        {
            Models.Home home = new Models.Home();
            return View(home);
        }
    }
}