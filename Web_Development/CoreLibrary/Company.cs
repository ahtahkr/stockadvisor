using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.CoreLibrary.Database
{
    public class Companies
    {
        private const string DB_STORED_PROCEDURE_GET_COMPANIES = "[dbo].[Company_Get_Filed]";
        private const string DB_STORED_PROCEDURE_GET_COMPANY_ROBINHOOD = "[dbo].[Company_Get_Robinhood]";
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

        public void Get_Company_Robinhood(int _open = 200)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("Open", _open);
            string All_Shares = Library.Database.ExecuteProcedure_Get( DB_STORED_PROCEDURE_GET_COMPANY_ROBINHOOD, param, Database_Connection_String);
            this._companies = JsonConvert.DeserializeObject<List<Company>>(All_Shares);
        }
    }

    public class Company
    {
        public const string DB_STORED_PROCEDURE_UPDATE_ROBINHOOD = "[dbo].[Company_Update_Robinhood]";
        public string Database_Connection_String { get; set; }
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

        public int Update_Robinhood()
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Ticker", this.Ticker);
                string result = Library.Database.ExecuteProcedure_Get(
                    DB_STORED_PROCEDURE_UPDATE_ROBINHOOD
                    , param
                    , Database_Connection_String);
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                } else
                {
                    return 1;
                }
                //this.Robinhood = !this.Robinhood;
            } catch (Exception ex) {
                Logger.Log_Error("AustinsFirstProject.CoreLibrary.Database.Company.UPDATE_ROBINHOOD failed. Error Message: ", ex.Message);
            }

            return 2;
        }            
    }
}
