using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.Github_Api.Model
{
    public class Commits
    {
        public List<CommitEvent> Commit_Event_List { get; set; }
        public int Repository_Id { get; set; }

        public Commits()
        {
            this.Commit_Event_List = new List<CommitEvent>();
        }
    }
}
