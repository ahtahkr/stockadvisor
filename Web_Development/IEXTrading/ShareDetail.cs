using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AustinsFirstProject.StockAdvisor.IEXTrading
{
    public class ShareDetails
    {
        public List<ShareDetail> ShareDetail { get; set; } = new List<ShareDetail>();
        public void Save_In_File(string directory = "")
        {
            string full_file_name = "";
            try
            {
                do
                {
                    full_file_name = Utility.Get_Full_FileName_to_Save_Api_Result(directory, "ShareDetail" + "_"
                        + ((this.ShareDetail[0].Symbol.Length > 0) ? this.ShareDetail[0].Symbol : "")
                        );
                } while (File.Exists(full_file_name));

                File.AppendAllText(full_file_name, JsonConvert.SerializeObject(this.ShareDetail));
            } catch (Exception ex)
            {
                /* MethodFullName. */
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Full_File_Name: [" + full_file_name + "] Error Msg: " + ex.Message);
            }
        }
    }
    public class ShareDetail
    {
        public string Symbol { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal  Open { get; set; } = 0;
        public decimal High { get; set; } = 0;
        public decimal Low { get; set; } = 0;
        public decimal Close { get; set; } = 0;
        public int Volume { get; set; } = 0;
        public int UnadjustedVolume { get; set; } = 0;
        public decimal Change { get; set; } = 0;
        public decimal ChangePercent { get; set; } = 0;
        public decimal Vwap { get; set; } = 0;

        public int Save_in_Database()
        {
            return this.IEXTrading_Share_Insert_Update();
        }

        public int IEXTrading_Share_Insert_Update(string connection_string = "")
        {
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

                Logger.Log_Error("[AustinsFirstProject.StockAdvisor.IEXTrading.ShareDetail.IEXTrading_Share_Insert_Update] failed. Param: [" + s + "] Message: " + ex.Message);
            }

            return 1;

        }
    }
}
