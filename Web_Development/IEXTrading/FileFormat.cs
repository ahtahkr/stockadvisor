using System;
using System.Collections.Generic;
using System.Text;

namespace AustinsFirstProject.StockProcessor.IEXTrading
{
    class FileFormat
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Close { get; set; }
    }
}
