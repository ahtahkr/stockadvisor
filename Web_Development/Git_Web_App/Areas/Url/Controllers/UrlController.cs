using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AustinsFirstProject.Git_Web_App.Classes;
using AustinsFirstProject.Library;
using Newtonsoft.Json;

namespace AustinsFirstProject.Git_Web_App.Areas.Url.Controllers
{
    [Area("Url")]
    [Route("Url")]
    public class UrlController : Controller
    {
        [Route("")]
        [Route("Url")]
        public IActionResult Index()
        {
            Models.Url url = new Models.Url();
            url.Git_Name_and_Url = SQLiteDB.Database.GetNameUrl();

            return View(url);
        }

        [HttpPost]
        public RedirectToRouteResult Create_Git_Object(Models.Url _url)
        {
            _url.Insert_Git_Name_and_Url();
            return RedirectToRoute(null);
        }
    }
}