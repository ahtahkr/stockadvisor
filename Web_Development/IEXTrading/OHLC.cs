﻿using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StockAdvisor.IEXTrading
{

    public class Open
    {
        public decimal Price { get; set; }
        public long Time { get; set; }
    }
    public class Close
    {
        public decimal Price { get; set; }
        public long Time { get; set; }
    }

    public class OHLC
    {
        public string Symbol { get; set; }
        public Open Open { get; set; }
        public Close Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }

        public bool Api_Called { get; set; } = false;

        public bool Call_Api(string symbol)
        {
            try
            {
                this.Symbol = symbol;
                OHLC data = JsonConvert.DeserializeObject<OHLC>(
                                Utility.HttpRequestor.OHLC(symbol));
                this.Open = data.Open;
                this.High = data.High;
                this.Low = data.Low;
                this.Close = data.Close;

                this.Api_Called = true;
                return true;
            } catch (Exception ex)
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
                Logger.Log_Error("StockAdvisor.IEXTrading.OHLC.Save_In_File(" + directory + ") failed. Error Msg: Calling Save to file without calling call_api");
                return;
            }

            try
            {
                ShareDetail ShareDetail = new ShareDetail
                {
                    Symbol = this.Symbol,
                    High = this.High,
                    Low = this.Low,
                    Open = this.Open.Price,
                    Close = this.Close.Price,
                    Date = (new DateTime(1970, 01, 01)).AddMilliseconds(this.Close.Time)
                };

                ShareDetails ShareDetails = new ShareDetails();
                ShareDetails.ShareDetail.Add(ShareDetail);
                ShareDetails.Save_In_File(directory);
            }
            catch (Exception ex)
            {
                Logger.Log_Error("StockAdvisor.IEXTrading.OHLC.Save_In_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }
    }
}
