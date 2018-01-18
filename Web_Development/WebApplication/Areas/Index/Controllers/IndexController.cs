using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Index.Controllers
{
    [Area("Index")]
    [Route("index")]
    public class IndexController : Controller
    {
        public string Index()
        {
            return "Index()";
        }

        [Route("[action]/{page:int?}")]
        public string Orders()
        {
            return "Orders";
        }

        [Route("[action]")]
        public string Shop()
        {
            return "Shop";
        }

        [Route("[action]/newest")]
        public string Payments()
        {
            return "Payments";
        }
    }
}