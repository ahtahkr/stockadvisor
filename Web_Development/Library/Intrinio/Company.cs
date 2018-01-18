using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Library.Intrinio
{
    public class Company
    {
        public string ticker { get; set; }
        public string name { get; set; }
        public string lei { get; set; }
        public string cik { get; set; }
        public DateTime? latest_filing_date { get; set; }

        public const string DB_STORED_PROCEDURE = "[dbo].[Company_Insert_Update]";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public int Save_in_Database()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            DateTime dt = latest_filing_date ?? new DateTime(1970, 01, 01);

            parameters.Add("ticker", ticker);
            parameters.Add("name", name);
            parameters.Add("lei", lei);
            parameters.Add("cik", cik);
            parameters.Add("latest_filing_date", latest_filing_date );
            
            return Database.ExecuteProcedure_Post(DB_STORED_PROCEDURE, parameters);
        }        
    }
}
