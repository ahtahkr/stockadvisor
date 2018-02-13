using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockProcessor.IEXTrading
{
    class FileFormats
    {
        public List<FileFormat> FileFormat { get; set; } = new List<FileFormat>();
        public void Save_to_File(string directory = "")
        {
            directory = Utility.Get_Full_FileName_to_Save_Api_Result(directory);

            File.AppendAllText(directory, JsonConvert.SerializeObject(this.FileFormat));
        }
    }
    class FileFormat
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
    }
}
