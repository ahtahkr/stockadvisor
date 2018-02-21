using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public bool IEXTrading_Get_Symbol_For_Chart(string connection_string = "")
        {
            string result = "Before.";
            try
            {
                result = Library.Database.ExecuteProcedure_Get(
                        "[fsn].[Get_Symbol_For_Chart]", null, connection_string);

                if (result.Equals("[]"))
                { return false; }
                else
                {
                    result = result.Split('[')[1];
                    result = result.Split(']')[0];
                    dynamic jsonparse = JObject.Parse(result);

                    this.Symbol = jsonparse["Symbol"];
                    this.Range = "";

                    if (String.IsNullOrEmpty(this.Symbol))
                    {
                        /* MethodFullName. */
                        Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Symbol: [" + this.Symbol + "] Range: [" + this.Range + "] Result: " + result);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] result = [" + result + "] Error Msg: " + ex.Message);
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

        public void DB_Invalid_Symbol_Date(Dictionary<string, object> parameters, string connection_string = "")
        {
            string result = "Before.";
            try
            {
                Library.Database.ExecuteProcedure_Get(
                        "[fsn].[Symbol_Invalid_Insert]", parameters, connection_string);
                result = "DONE";
            }
            catch (Exception ex)
            {
                Logger.Log_Error("[AustinsFirstProject.StockAdvisor.IEXTrading.Chart.DB_Invalid_Symbol_Date] result = [" + result + "] Error Msg: " + ex.Message);
            }

        }

        public bool Call_Api_Date(string symbol, string date, bool last_record = false)
        {
            this.Symbol = symbol;

            try
            {
                DateTime _date = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);

                string result = Utility.HttpRequestor.Chart(this.Symbol, "date/" + date);

                if (String.IsNullOrEmpty(result))
                {
                    DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "Symbol", this.Symbol },
                            { "Date", dt }
                        };
                    this.DB_Invalid_Symbol_Date(parameters);
                    return false;
                } else
                {
                    if (result.Equals("[]"))
                    {
                        DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "Symbol", this.Symbol },
                            { "Date", dt }
                        };
                        this.DB_Invalid_Symbol_Date(parameters);
                        return false;
                    } else {

                        List<ShareDetail> share_list = JsonConvert.DeserializeObject<List<ShareDetail>>(
                                                            result
                                                            , new IsoDateTimeConverter { DateTimeFormat = "yyyyMMdd" }
                                                        );
                        share_list[share_list.Count - 1].Date = _date;
                        share_list[share_list.Count - 1].Close = share_list[share_list.Count - 1].High;
                        share_list[share_list.Count - 1].Symbol = this.Symbol;

                        this.ShareDetails.ShareDetail = new List<ShareDetail>(1);
                        this.ShareDetails.ShareDetail.Add(share_list[share_list.Count - 1]);
                        this.Api_Called = true;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                /* MethodFullName. */
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Symbol: [" + symbol + "] date: [" + date + "] Error Msg: " + ex.Message);

                DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                Dictionary<string, object> parameters = new Dictionary<string, object>
                        {
                            { "Symbol", this.Symbol },
                            { "Date", dt }
                        };
                this.DB_Invalid_Symbol_Date(parameters);
                return false;
            }
        }

        public bool Call_Api(string symbol = "", string range = "")
        {
            if (!String.IsNullOrEmpty(symbol))
            {
                this.Symbol = symbol;
            }

            if (!String.IsNullOrEmpty(range))
            {
                this.Range = range;
            }

            if (String.IsNullOrEmpty(this.Symbol))
            {
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Failed. Symbol is empty.");
                return false;
            }

            try
            {
                this.ShareDetails.ShareDetail = JsonConvert.DeserializeObject<List<ShareDetail>>(
                                    Utility.HttpRequestor.Chart(this.Symbol, this.Range)
                                    );
                this.ShareDetails.ShareDetail.ForEach(sharedetail => sharedetail.Symbol = this.Symbol);

                this.Api_Called = true;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Call_Api(" + symbol + ") failed. Error Msg: " + ex.Message);
                return false;
            }
        }

        public bool Save_In_File(string directory = "")
        {
            return this.ShareDetails.Save_In_File();
        }
    }
}
