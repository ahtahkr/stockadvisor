using Newtonsoft.Json;
using System;

namespace AustinStockAdvisor.Library
{
    public class Change
    {
        public string Symbol { get; set; }
        public int WeeksToGoBack { get; set; }
        public int Max_High { get; set; }
        public string Url { get; set; }
        public int Avg_Volume { get; set; }
    }

    public class Share
    {
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public string WeekDay { get; set; }

        public string Share_Insert_Update(string connection_string)
        {
            System.Collections.Generic.Dictionary<string, object> parameters
                = new System.Collections.Generic.Dictionary<string, object>();


            parameters.Add("symbol", this.Symbol);
            parameters.Add("date", this.Date);
            parameters.Add("open", this.Open);
            parameters.Add("high", this.High);
            parameters.Add("low", this.Low);
            parameters.Add("close", this.Close);
            parameters.Add("volume", this.Volume);
            parameters.Add("change", this.Change);
            parameters.Add("changePercent", this.ChangePercent);

            return AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Insert_Update]",
                connection_string,
                parameters
            );
        }

        

        

        /*
        public int unadjustedVolume { get; set; }
        public double vwap { get; set; }
        public string label { get; set; }
        public double changeOverTime { get; set; }
        */
    }
    public class Company
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool isEnabled { get; set; }
        public string Type { get; set; }
        public int IexId { get; set; }
        public int Iex_Chart_3m { get; set; }
        public bool Iex_Trading { get; set; }
        public bool Robinhood { get; set; }

        public Company() { }

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

        public void Company_Update_IEX_Chart_3M(string connection_string)
        {
            System.Collections.Generic.Dictionary<string, object> parameters
                  = new System.Collections.Generic.Dictionary<string, object>();

            parameters.Add("Symbol", this.Symbol);

            AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Company_Update_IEX_Chart_3M]",
                connection_string,
                parameters
            );

        }

        public void Company_Alter_IEX_Trading(string connection_string)
        {
            System.Collections.Generic.Dictionary<string, object> parameters
                      = new System.Collections.Generic.Dictionary<string, object>();
            /* MethodFullName. */
            string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";

            parameters.Add("Symbol", this.Symbol);
            try
            {
                AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                    "[fsn].[Company_Alter_IEX_Trading]",
                    connection_string,
                    parameters
                );
            } catch (Exception ex)
            {
                
                Library.Logger.Log_Error(methodfullname + " . Error Msg: " + ex.Message + ". Connection String: [" + connection_string + "] Shares List: [" + JsonConvert.SerializeObject(parameters) + "]");
            }

        }
    }
}
