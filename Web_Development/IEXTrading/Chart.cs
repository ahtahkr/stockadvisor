using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockAdvisor.IEXTrading
{
    public class Chart
    {
        public ShareDetails ShareDetails { get; set; }
        public string Symbol { get; set; }
        public string Range { get; set; }
        public bool Api_Called { get; set; } = false;

        public Chart()
        {
            this.ShareDetails = new ShareDetails();
        }

        public bool Set_Symbol_Range_from_DB(string connection_string = "")
        {
            string result = "Before.";
            try
            {
                result = Library.Database.ExecuteProcedure_Get(
                        "[fsn].[IEXTrading_Get_Symbol_For_Chart]", null, connection_string);

                result = result.Split('[')[1];
                result = result.Split(']')[0];
                dynamic jsonparse = JObject.Parse(result);

                this.Symbol = jsonparse["Symbol"];
                this.Range = jsonparse["Range"];
                return true;
            } catch (Exception ex)
            {
                Logger.Log_Error("[AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Set_Symbol_Range_from_DB] result = [" + result + "] Error Msg: " + ex.Message);
                return false;
            }
        }

        public string Download_Chart_Date(string connection_string = "")
        {
            string result = "Before.";
            try
            {
                result = Library.Database.ExecuteProcedure_Get(
                        "[fsn].[Get_Symbol_Date]", null, connection_string);

                result = result.Split('[')[1];
                result = result.Split(']')[0];

                return result;              
                
            }
            catch (Exception ex)
            {
                Logger.Log_Error("[AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Download_Chart_Date] result = [" + result + "] Error Msg: " + ex.Message);
                return "";
            }

        }

        public bool Call_Api_Date(string symbol, string date, bool last_record = false)
        {
            this.Symbol = symbol;

            string result = "";
            try
            {
                result = Utility.HttpRequestor.Chart(this.Symbol, "date/" + date);

                if (!String.IsNullOrEmpty(result))
                {
                    if (last_record)
                    {
                        string[] results = result.Split('{');
                        result = results[results.Length - 1];
                        result = "[{" + result.Split(']')[0] + "]";
                    }

                    this.ShareDetails.ShareDetail = JsonConvert.DeserializeObject<List<ShareDetail>>(
                                        result
                                    , new IsoDateTimeConverter { DateTimeFormat = "yyyyMMdd" }
                                        );

                    this.Api_Called = true;
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Call_Api_Date(" + symbol+", "+ "date/" + date + ") failed. Result: [" + result + "] Error Msg: " + ex.Message);
                return false;
            }
        }

        public bool Call_Api(string symbol = "", string range = "")
        {
            if (String.IsNullOrEmpty(symbol))
            {
                //this.Set_Symbol_Range_from_DB();
            }
            else
            {
                this.Range = range;
                this.Symbol = symbol;
            }
            try
            {
                this.ShareDetails.ShareDetail = JsonConvert.DeserializeObject<List<ShareDetail>>(
                                    Utility.HttpRequestor.Chart(this.Symbol, this.Range)
                                    );

                this.Api_Called = true;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Call_Api(" + symbol + ") failed. Error Msg: " + ex.Message);
                return false;
            }
        }

        public void Save_to_File(string directory = "")
        {
            try
            {
                this.ShareDetails.Save_to_File();
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Save_to_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }
    }
}
