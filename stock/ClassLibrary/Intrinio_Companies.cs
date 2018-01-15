using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;

namespace Library
{
    class Intrinio_Companies
    {
        public string data { get; set; }
        public int result_count { get; set; }
        public int page_size { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public int api_call_credits { get; set; }

        public void Get_all_Companies()
        {
            HttpRequestUtility http = new HttpRequestUtility();
            Configuration
            http.GetRequest();
        }
    }
}
