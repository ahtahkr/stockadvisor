using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Stock
{
    public class Ticker_Class
    {
        public string Ticker { get; set; }
    }

    public class Tickers_Class
    {
        public List<Ticker_Class> Tickers { get; set; }

        public Tickers_Class()
        {
            this.Tickers = new List<Ticker_Class>();
        }
    }
}
