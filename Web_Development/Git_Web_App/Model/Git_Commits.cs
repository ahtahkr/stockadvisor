using AustinsFirstProject.Git_Web_App.Model;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Classes
{
    public class Git_Commit
    {
        public Author Author { get; set; }

        public string Message { get; set; }

        public string MessageShort { get; set; }

        public Git_Commit()
        {
            this.Author = new Author();
        }

        public void Set_Message(string msg = "", string msg_short = "")
        {
            if (String.IsNullOrEmpty(msg) && String.IsNullOrEmpty(msg_short))
            {
                return;
            } else
            {
                this.Message = msg;
                this.MessageShort = msg_short;
            }
        }

        public void Set_Author(Signature signature)
        {
            this.Author.Email = signature.Email;
            this.Author.Name = signature.Name;
            this.Author.When = signature.When;
        }
    }

    public class Git_Commits
    {
        public List<Git_Commit> Git__Commits { get; set; }

        public Git_Commits()
        {
            this.Git__Commits = new List<Git_Commit>();
        }
    }
}