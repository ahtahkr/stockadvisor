using AustinsFirstProject.Git_Web_App.Classes;
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
        public string Name { get; set; }
        public Repository repository { get; set; }
        public bool IsValid { get; set; }

        public Git_Commits Git_Commits { get; set; }

        public Git_Url_Basic_Info()
        {
            this.Git_InValid_Protocol();
        }
        
        private void Get_Name_from_Database()
        {
            this.Name = SQLiteDB.Database.Get_Name_of_Url(this.Url);
        }

        public Git_Url_Basic_Info(string git_name, string git_url)
        {
            if (Repository.IsValid(git_url))
            {
                this.IsValid = true;
                this.Url = git_url;
                if (String.IsNullOrEmpty(git_name))
                {
                    this.Get_Name_from_Database();
                }
                else
                {
                    this.Name = git_name;
                }
                this.repository = new Repository(git_url);
                this.Git_Commits = new Git_Commits();
                
                foreach ( Commit commit in this.repository.Commits)
                {
                    Git_Commit git_Commit = new Git_Commit();
                    git_Commit.Set_Author(commit.Author);
                    git_Commit.Set_Message(commit.Message, commit.MessageShort);
                    git_Commit.Sha = commit.Sha;
                    git_Commit.Repo_Path = git_url;

                    this.Git_Commits.Git__Commits.Add(git_Commit);
                }
            }
            else
            {
                //Logger.Log_Error("Git Url: [" + git_url + "] not a valid repository.", "Invalid_Repository");
                this.Git_InValid_Protocol();
            }
        }

        private void Git_InValid_Protocol()
        {
            this.IsValid = false;
            this.Name = "Invalid Repository";
            this.Url = "Invalid Repository";
            this.repository = null;
        }
    }
}
