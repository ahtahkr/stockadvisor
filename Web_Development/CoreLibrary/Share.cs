using AustinsFirstProject.CoreLibrary.Modal;
using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AustinsFirstProject.CoreLibrary.Database
{
    public class Shares
    {
        private const string DB_STORED_PROCEDURE_GET_ALL_SHARES = "[dbo].[Share_Get]";
        private const string DB_STORED_PROCEDURE_GET_TICKER = "[dbo].[Share_Get_Ticker]";
        public string Database_Connection_String;
        public string Identity;

        public List<Share> _shares { get; set; }

        public Shares()
        {
            this._shares = new List<Share>();
        }
        public int Shares_Count() { return this._shares.Count;  }

        public string Shares_Date_Close()
        {
            List<DataPoint> data_points = new List<DataPoint>();
            JArray result = new JArray();

            for (int a = 0; a < this._shares.Count; a++)
            {

                data_points.Add(
                    new DataPoint(
                            (Convert.ToInt64(this._shares[a].Unix_Timestamp) * 1000)
                            , Convert.ToDouble(this._shares[a].close.ToString())
                        )
                    );
            }
            return JsonConvert.SerializeObject(data_points);
        }

        public void Get_Ticker(string ticker)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ticker", ticker);
            string All_Shares = Library.Database.ExecuteProcedure_Get(DB_STORED_PROCEDURE_GET_TICKER, parameters, Database_Connection_String);
            this._shares = JsonConvert.DeserializeObject<List<Share>>(All_Shares);
        }

        public void Get_All_Shares()
        {
            string All_Shares = Library.Database.ExecuteProcedure_Get(DB_STORED_PROCEDURE_GET_ALL_SHARES, new Dictionary<string, object>(), Database_Connection_String);
            this._shares = JsonConvert.DeserializeObject<List<Share>>(All_Shares);
        }
    }

    public class Share
    {
        public Share(string _ticker
                        , int _unix_timestamp = 0
                        , int _id = 0
                        , DateTime _date = new DateTime()
                        , decimal _open = 0
                        , decimal _high = 0
                        , decimal _low = 0
                        , decimal _close = 0
                        , int _volume = 0)
        {
            this.Ticker = _ticker;
            this.ID = ID;
            this.open = _open;
            this.high = _high;
            this.low = _low;
            this.close = _close;
            this.volume = _volume;
            this.Unix_Timestamp = _unix_timestamp;
        }
        private const string DB_STORED_PROCEDURE_GET = "[dbo].[Share_Insert_Update]";

        public int ID { get; set; }
        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public int volume { get; set; }
        public string Ticker { get; set; }

        public int Unix_Timestamp { get; set; }
    }
}
