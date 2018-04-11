using System;
using System.Collections.Generic;

namespace ProductionDatabase
{
    public class Share
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
        public int UnadjustedVolume { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public double Vwap { get; set; }

        public string Save_in_Database(string connection_string)
        {
            Dictionary<string, object> parameters
                = new Dictionary<string, object>
                {
                    { "symbol", this.Symbol },
                    { "date", this.Date },
                    { "open", this.Open },
                    { "high", this.High },
                    { "low", this.Low },
                    { "close", this.Close },
                    { "volume", this.Volume },
                    { "change", this.Change },
                    { "changePercent", this.ChangePercent },
                    { "vwap", this.Vwap }
                };

            return Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Insert_Update]",
                connection_string,
                parameters
            );
        }
    }
}
