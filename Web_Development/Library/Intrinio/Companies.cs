using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Library.Intrinio;
using Newtonsoft.Json;

namespace AustinsFirstProject.Library.Intrinio
{
    public class Companies
    {
        public List<Company> data { get; set; }
        public int result_count { get; set; }
        public int page_size { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public int api_call_credits { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Save_in_Database()
        {
            for (int a = 0; a < data.Count; a++)
            {
                data[a].Save_in_Database();
            }
        }
    }
}
