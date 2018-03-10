using System;

namespace AustinStockAdvisor.Library
{
    public class Company
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool isEnabled { get; set; }
        public string Type { get; set; }
        public int IexId { get; set; }

        public string Company_Insert_Update(string connection_string)
        {
            System.Collections.Generic.Dictionary<string, object> parameters
                = new System.Collections.Generic.Dictionary<string, object>();

            parameters.Add("symbol", this.Symbol);
            parameters.Add("name", this.Name);
            parameters.Add("date", this.Date);
            parameters.Add("isEnabled", this.isEnabled);
            parameters.Add("type", this.Type);
            parameters.Add("iexid", this.IexId);

            return AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Company_Insert_Update]",
                connection_string,
                parameters
            );
        }
    }
}
