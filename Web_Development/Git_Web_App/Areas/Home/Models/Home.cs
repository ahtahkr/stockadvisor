using AustinsFirstProject.Git_Web_App.Model;
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
        
        public List<Dictionary<string, Git_Url_Basic_Info>> Basic_Info { get; set; }

        public Home()
        {
            this.Name_Url = new List<Dictionary<string, string>>();
            this.Basic_Info = new List<Dictionary<string, Git_Url_Basic_Info>>();

            this.Get_Name_Url();
            this.SetUp_Basic_Info();
        }

        public void SetUp_Basic_Info()
        {
            foreach (var item in this.Name_Url)
            {
                Dictionary<string, Git_Url_Basic_Info> dictionary = new Dictionary<string, Git_Url_Basic_Info>(1);
                dictionary.Add(item["name"], new Git_Url_Basic_Info(item["name"], item["url"]));
                this.Basic_Info.Add(dictionary);
            }
        }

        public void Get_Name_Url()
        {
            this.Name_Url = SQLiteDB.Database.GetNameUrl();

        }

        public string Get_Title()
        {
            return Title;
        }
    }
}
