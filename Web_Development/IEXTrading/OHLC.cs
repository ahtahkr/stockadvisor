using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockProcessor.IEXTrading
{

    public class Open
    {
        public double Price { get; set; }
        public long Time { get; set; }
    }
    public class Close
    {
        public double Price { get; set; }
        public long Time { get; set; }
    }

    public class OHLC
    {
        public string Symbol { get; set; }
        public Open Open { get; set; }
        public Close Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }

        public bool Api_Called { get; set; } = false;

        public bool Call_Api(string symbol)
        {
            try
            {
                this.Symbol = symbol;
                OHLC data = JsonConvert.DeserializeObject<OHLC>(
                                Utility.HttpRequestor.OHLC(symbol));
                this.Open = data.Open;
                this.Close = data.Close;
                this.High = data.High;
                this.Low = data.Low;
                this.Api_Called = true;
                return true;
            } catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.OHLC.Call_Api(" + symbol + ") failed. Error Msg: " + ex.Message);
                return false;
            }
        }

        public void Save_to_File(string directory = "")
        {
            if (!Api_Called)
            {
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.OHLC.Save_to_File(" + directory + ") failed. Error Msg: Calling Save to file without calling call_api");
                return;
            }

            try
            {
                directory = Utility.Get_Full_FileName_to_Save_Api_Result(directory);

                FileFormat fileFormat = new FileFormat();
                fileFormat.Symbol = this.Symbol;
                fileFormat.Close = this.Close.Price;
                fileFormat.Date = (new DateTime(1970, 01, 01)).AddMilliseconds(this.Close.Time);
                
                File.AppendAllText(directory, JsonConvert.SerializeObject(fileFormat) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.OHLC.Save_to_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }

    }
}
