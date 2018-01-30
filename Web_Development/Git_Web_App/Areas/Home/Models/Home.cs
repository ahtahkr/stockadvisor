using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Areas.Home.Models
{
    public class Home
    {
        public const string Title = "Home";

        public List<Dictionary<string, string>> Name_Url { get; set; }

        public string Get_Title()
        {
            return Title;
        }
    }
}
