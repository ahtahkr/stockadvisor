using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AustinsFirstProject.WebApplication.Areas.Home.Controllers
{
    [Area("Home")]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}