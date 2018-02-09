using AustinsFirstProject.Library;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Model
{
    public class Committers
    {
        public List<Author> Authors { get; set; }

        public Repository Repository { get; set; }

        public string Repo_Url { get; set; }

        public Committers()
        {
            this.Authors = new List<Author>();
        }

        public Committers(string git_url = "")
        {
            this.Authors = new List<Author>();
            this.Set_All_Committers(git_url);
        }

        public bool Committer_Exists(Signature signature)
        {
            for (int a = 0; a < this.Authors.Count; a++)
            {
                if (this.Authors[a].Email == signature.Email)
                {
                    return true;
                }
            }
            return false;
        }

        public void Add_Committer(Signature signature)
        {
            if (this.Committer_Exists(signature))
            {
                return;
            }
            Author author = new Author();
            author.Email = signature.Email;
            author.Name = signature.Name;
            author.When = signature.When;

            this.Authors.Add(author);
        }

        public void Set_All_Committers(string git_url)
        {
            if (Repository.IsValid(git_url))
            {
                //if (String.IsNullOrEmpty(git_name))
                //{
                //    this.Get_Name_from_Database();
                //}
                //else
                //{
                //    this.Name = git_name;
                //}
                this.Repo_Url = git_url;
                Logger.Log(git_url, "Set_All_Committers");
                this.Repository = new Repository(git_url);

                foreach (Commit commit in this.Repository.Commits)
                {
                    this.Add_Committer(commit.Committer);
                }
            }
            else
            {
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Git Url: [" + git_url + "] not a valid repository in ");
            }
        }
    }
}
