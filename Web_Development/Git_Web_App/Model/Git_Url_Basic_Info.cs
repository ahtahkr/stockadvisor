using AustinsFirstProject.Library;
using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Model
{
    public class Git_Url_Basic_Info
    {
        public string Url { get; set; } 
        public Repository repository { get; set; }
        public bool IsValid { get; set; }

        public Git_Url_Basic_Info(string git_url)
        {
            if (Repository.IsValid(git_url))
            {
                this.IsValid = true;
                this.Url = git_url;
                this.repository = new Repository(git_url);
                //Logger.Log(JsonConvert.SerializeObject(this.repository), "Repository");
            }
            else
            {
                Logger.Log_Error("Git Url: [" + git_url + "] not a valid repository.", "Invalid_Repository");
                this.Git_InValid_Protocol();
            }
        }

        private void Git_InValid_Protocol()
        {
            this.IsValid = false;
        }
    }
}
