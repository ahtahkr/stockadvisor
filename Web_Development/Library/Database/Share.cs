using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.Library.DatabaseTable
{
    public class Share
    {
        private const string DB_STORED_PROCEDURE = "[dbo].[Share_Insert_Update]";
        public DateTime _date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public int volume { get; set; }
        public string ticker { get; set; }

        public object Save_in_Database()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("ticker", ticker);
            parameters.Add("_date", _date);
            parameters.Add("open", open);
            parameters.Add("high", high);
            parameters.Add("low", low);
            parameters.Add("close", close);
            parameters.Add("volume", volume);

            return Database.ExecuteProcedure_Get(DB_STORED_PROCEDURE, parameters);
            
        }
    }
}
