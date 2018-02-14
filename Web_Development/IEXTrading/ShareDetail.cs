using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockAdvisor.IEXTrading
{
    public class ShareDetails
    {
        public List<ShareDetail> ShareDetail { get; set; } = new List<ShareDetail>();
        public void Save_to_File(string directory = "")
        {
            string full_file_name;

            do
            {
                full_file_name = Utility.Get_Full_FileName_to_Save_Api_Result(directory, "ShareDetail" + "_" 
                    + ((this.ShareDetail[0].Symbol.Length > 0) ? this.ShareDetail[0].Symbol : "")
                    );
            } while (File.Exists(full_file_name));

            File.AppendAllText(full_file_name, JsonConvert.SerializeObject(this.ShareDetail));
        }
    }
    public class ShareDetail
    {
        public string Symbol { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public double Open { get; set; } = 0;
        public double High { get; set; } = 0;
        public double Low { get; set; } = 0;
        public double Close { get; set; } = 0;
        public int Volume { get; set; } = 0;
        public int UnadjustedVolume { get; set; } = 0;
        public double Change { get; set; } = 0;
        public double ChangePercent { get; set; } = 0;
        public double Vwap { get; set; } = 0;

        public void Save_in_Database()
        {
            this.IEXTrading_Share_Insert_Update();
        }

        public int IEXTrading_Share_Insert_Update(string connection_string = "")
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
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
                Logger.Log_Error("[AustinsFirstProject.StockAdvisor.IEXTrading.ShareDetail.IEXTrading_Share_Insert_Update] failed. Message: " + ex.Message);
            }

            return 1;

        }
    }
}
