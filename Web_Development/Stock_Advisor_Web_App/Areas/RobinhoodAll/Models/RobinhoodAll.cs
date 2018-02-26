using CoreLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Areas.RobinhoodAll.Models
{
    public class RobinhoodAll
    {
        public int Pages { get; set; }

        public int Open { get; set; }

        public Companies _Companies { get; set; }

        public RobinhoodAll() { }
    }
}
