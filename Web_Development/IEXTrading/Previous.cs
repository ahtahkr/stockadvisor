using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockProcessor.IEXTrading
{
    public class Previous
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
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.OHLC.Call_Api(" + symbol + ") failed. Error Msg: " + ex.Message);
                return false;
            }
        }

        public void Save_to_File(string directory = "")
        {
            if (!Api_Called)
            {
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.Previous.Save_to_File(" + directory + ") failed. Error Msg: Calling Save to file without calling call_api");
                return;
            }

            try
            {
                FileFormats fileFormats = new FileFormats();

                FileFormat fileFormat = new FileFormat();
                fileFormat = JsonConvert.DeserializeObject<FileFormat>(
                                JsonConvert.SerializeObject(this));

                fileFormats.FileFormat.Add(fileFormat);
                fileFormats.Save_to_File(directory);
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.Previous.Save_to_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }
    }
}
