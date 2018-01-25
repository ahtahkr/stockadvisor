using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.CoreLibrary.Database
{
    public class Companies
    {
        private const string DB_STORED_PROCEDURE_GET_COMPANIES = "[dbo].[Company_Get_Filed]";
        public List<Company> _companies { get; set; }
        public string Database_Connection_String { get; set; }

        public Companies()
        {
            this._companies = new List<Company>();
        }

        public void Get_Company_Filed()
        {
            string companies = Library.Database.ExecuteProcedure_Get(DB_STORED_PROCEDURE_GET_COMPANIES, new Dictionary<string, object>(), Database_Connection_String);
            this._companies = JsonConvert.DeserializeObject<List<Company>>(companies);
        }
    }

    public class Company
    {
        public int ID { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Lei { get; set; }
        public string Cik { get; set; }
        public DateTime Latest_Filing_Date { get; set; }
        public int Next_In_Line { get; set; }
        public int Update_Timestamp { get; set; }
        public int Share_Updated { get; set; }
        public bool Robinhood { get; set; }
    }
}
