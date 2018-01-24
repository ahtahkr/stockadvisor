using System;
using System.Collections.Generic;

namespace AustinsFirstProject.CoreLibrary.Database
{
    public class Shares
    {
        public List<Share> _shares { get; set; }

        public Shares()
        {
            this._shares = new List<Share>();
        }
        public int Shares_Count() { return this._shares.Count;  }
    }

    public class Share
    {
        public Share(string _ticker
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
    }
}
