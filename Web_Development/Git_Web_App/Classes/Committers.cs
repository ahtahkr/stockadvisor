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

        public Committers()
        {
            this.Authors = new List<Author>();
        }

        public void Add_Committer(Signature signature)
        {
            Author author = new Author();
            author.Email = signature.Email;
            author.Name = signature.Name;
            author.When = signature.When;

            this.Authors.Add(author);
        }
    }
}
