﻿using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StockAdvisor.IEXTrading
{
    public class ShareDetails
    {
        public List<ShareDetail> ShareDetail { get; set; } = new List<ShareDetail>();
        public bool Save_In_File(string directory = "")
        {
            if (this.ShareDetail.Count <= 0)
            {
                /* MethodFullName. */
                /*Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] this.ShareDetail.Count: [" + this.ShareDetail.Count + "] ShareDetail: #" + JsonConvert.SerializeObject(this.ShareDetail));*/
                return false;
            }
            string full_file_name = "";
            try
            {
                do
                {
                    full_file_name = Utility.Get_Full_FileName_to_Save_Api_Result(directory, "ShareDetail" + "_"
                        + ((this.ShareDetail[0].Symbol.Length > 0) ? this.ShareDetail[0].Symbol : "")
                        );
                    full_file_name = full_file_name.Replace("*", "");
                } while (File.Exists(full_file_name));

                File.AppendAllText(full_file_name, JsonConvert.SerializeObject(this.ShareDetail));
                return true;
            } catch (Exception ex)
            {
                /* MethodFullName. */
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Full_File_Name: [" + full_file_name + "] ShareDetail: #"+ JsonConvert.SerializeObject(this.ShareDetail) + "#Error Msg: " + ex.Message);
                return false;
            }
        }
    }
    public class ShareDetail
    {
        public string Symbol { get; set; } = "";
        public DateTime Date { get; set; }
        public decimal  Open { get; set; } = 0;
        public decimal High { get; set; } = 0;
        public decimal Low { get; set; } = 0;
        public decimal Close { get; set; } = 0;
        public int Volume { get; set; } = 0;
        public int UnadjustedVolume { get; set; } = 0;
        public decimal Change { get; set; } = 0;
        public decimal ChangePercent { get; set; } = 0;
        public decimal Vwap { get; set; } = 0;

        public int Save_in_Database(string connection_string = "")
        {
            return this.IEXTrading_Share_Insert_Update(connection_string);
        }

        public int IEXTrading_Share_Insert_Update(string connection_string = "")
        {
            if ((this.Open >= 100000) || (this.High >= 100000) || (this.Low >= 100000) || (this.Close >= 100000))
            {
                return 0;
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            try
            {
                param.Add("Symbol", this.Symbol);
                param.Add("Date", this.Date);
                param.Add("Open", this.Open);
                param.Add("High", this.High);
                param.Add("Low", this.Low);
                param.Add("Close", this.Close);
                param.Add("Volume", this.Volume);
                param.Add("UnadjustedVolume", this.UnadjustedVolume);
                param.Add("Change", this.Change);
                param.Add("ChangePercent", this.ChangePercent);
                param.Add("Vwap", this.Vwap);

                string result = Library.Database.ExecuteProcedure_Get(
                    "[fsn].[Share_Insert_Update]"
                    , param, connection_string);
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string s = string.Join(";", param.Select(x => x.Key + "=" + x.Value).ToArray());

                Logger.Log_Error("[StockAdvisor.IEXTrading.ShareDetail.IEXTrading_Share_Insert_Update] failed. Param: [" + s + "] Message: " + ex.Message);
            }

            return 1;

        }
    }
}
