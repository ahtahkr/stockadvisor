﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Areas.Company.Models
{
    public class Company
    {
        public CoreLibrary.Database.Companies companies { get; set; }
        public string Title { get; set; }

        public Company()
        {
            this.companies = new CoreLibrary.Database.Companies();
            this.Title = "WebApplication.Areas.Company.Models.Company";
        }
    }
}
