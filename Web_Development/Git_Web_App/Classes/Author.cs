using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Model
{
    public class Author
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTimeOffset When { get; set; }
    }
}
