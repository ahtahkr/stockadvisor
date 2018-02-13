using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.StockAdvisor.IEXTrading
{
    public class Chart
    {
        public List<Previous> Previous { get; set; }
        public string Symbol { get; set; }
        public string Range { get; set; }
        public bool Api_Called { get; set; } = false;

        public Chart()
        {
            this.Previous = new List<Previous>();
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
                this.Previous = JsonConvert.DeserializeObject<List<Previous>>(
                                Utility.HttpRequestor.Chart(this.Symbol, this.Range));

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
                ShareDetails ShareDetails = new ShareDetails();
                ShareDetail ShareDetail = new ShareDetail();

                for (int a = 0; a < this.Previous.Count; a++)
                {
                    ShareDetail = JsonConvert.DeserializeObject<ShareDetail>(
                                    JsonConvert.SerializeObject(this.Previous[a]));
                    ShareDetail.Symbol = this.Symbol;
                    ShareDetails.ShareDetail.Add(ShareDetail);
                    ShareDetails.Save_to_File(directory);
                    ShareDetails.ShareDetail = new List<ShareDetail>();
                }

                
            }
            catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockAdvisor.IEXTrading.Chart.Save_to_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }
    }
}
