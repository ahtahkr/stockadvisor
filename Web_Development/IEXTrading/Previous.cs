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

        public void Save_to_File(string directory = "")
        {
            try
            {
                directory = Utility.Get_Full_FileName_to_Save_Api_Result(directory);

                FileFormat fileFormat = JsonConvert.DeserializeObject<FileFormat>(
                                            JsonConvert.SerializeObject(this));

                File.AppendAllText(directory, JsonConvert.SerializeObject(fileFormat) + Environment.NewLine);
            } catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockProcessor.IEXTrading.Previous.Save_to_File(" + directory + ") failed. Error Msg: " + ex.Message);
            }
        }
    }
}
