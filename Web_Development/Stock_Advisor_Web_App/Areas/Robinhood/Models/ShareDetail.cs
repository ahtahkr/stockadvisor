using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Areas.Robinhood.Models
{
    public class ShareDetail
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal Open { get; set; } = 0;
        public decimal High { get; set; } = 0;
        public decimal Low { get; set; } = 0;
        public decimal Close { get; set; } = 0;
        public int Volume { get; set; } = 0;
        public int UnadjustedVolume { get; set; } = 0;
        public decimal Change { get; set; } = 0;
        public decimal ChangePercent { get; set; } = 0;
        public decimal Vwap { get; set; } = 0;
    }
}
