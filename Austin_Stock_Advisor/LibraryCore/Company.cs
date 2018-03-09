using System;

namespace AustinStockAdvisor.LibraryCore
{
    public class Company
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool isEnabled { get; set; }
        public string Type { get; set; }
        public int IexId
        {
            get; set;
        }
    }
}
