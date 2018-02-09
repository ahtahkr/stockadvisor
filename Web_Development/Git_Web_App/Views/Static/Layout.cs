using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Views.Static
{
    public class Layout
    {
        public List<Dictionary<string, string>> Name_and_Url { get; set; }

        public Layout()
        {
            this.Name_and_Url = new List<Dictionary<string, string>>();
            this.Get_Name_Url();
        }

        public void Get_Name_Url()
        {
            this.Name_and_Url = SQLiteDB.Database.GetNameUrl();
        }
    }
}
