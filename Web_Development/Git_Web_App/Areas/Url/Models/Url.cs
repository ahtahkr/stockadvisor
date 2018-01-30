using AustinsFirstProject.Git_Web_App.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Areas.Url.Models
{
    public class Url
    {
        public const string Title = "Url";

        public List<Dictionary<string, string>> Git_Name_and_Url { get; set; }

        public Git_Object git_object { get; set; }

        public string route { get; set; }

        public string Get_Title()
        {
            return Title;
        }

        public void Insert_Git_Name_and_Url()
        {
            SQLiteDB.Database.Execute_Query("insert into git_repos (name, url) values ('" + this.git_object.Git_Url_Name +"', '" + this.git_object.Git_Url + "')");
        }
    }
}
