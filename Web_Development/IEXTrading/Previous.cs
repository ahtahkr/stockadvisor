using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockAdvisor.IEXTrading
{
    public class Previous
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
        public int UnadjustedVolume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
        public decimal Vwap { get; set; }
        public bool Api_Called { get; set; } = false;

        public bool Call_Api(string symbol)
        {
            try
            {
                Previous data = JsonConvert.DeserializeObject<Previous>(
                                Utility.HttpRequestor.Previous(symbol));
                this.Symbol = data.Symbol;
                this.Date = data.Date;
                this.Open = data.Open;
                this.High = data.High;
                this.Low = data.Low;
                this.Close = data.Close;
                this.Volume = data.Volume;
                this.UnadjustedVolume = data.UnadjustedVolume;
                this.Change = data.Change;
                this.ChangePercent = data.ChangePercent;
                this.Vwap = data.Vwap;

                this.Api_Called = true;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log_Error(symbol + " : " + ex.Message, "Call_Api");
                Logger.Log_Error("StockAdvisor.IEXTrading.OHLC.Call_Api(" + symbol + ") failed. Error Msg: " + ex.Message);
                return false;
            }
        }

        public void Save_In_File(string directory = "")
        {
            if (!Api_Called)
            {
                Logger.Log_Error("StockAdvisor.IEXTrading.Previous.Save_In_File(" + directory + ") failed. Error Msg: Calling Save to file without calling call_api");
                return;
            }

            try
            {
                ShareDetails ShareDetails = new ShareDetails();

                ShareDetail ShareDetail = new ShareDetail();
                ShareDetail = JsonConvert.DeserializeObject<ShareDetail>(
                                JsonConvert.SerializeObject(this));

                ShareDetails.ShareDetail.Add(ShareDetail);
                ShareDetails.Save_In_File(directory);
            }
            catch (Exception ex)
            {
                Logger.Log_Error("StockAdvisor.IEXTrading.Previous.Save_In_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }
    }
}
