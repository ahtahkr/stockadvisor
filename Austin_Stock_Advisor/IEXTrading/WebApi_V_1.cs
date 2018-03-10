using System;

namespace AustinStockAdvisor.IEXTrading.WebApi
{
    public static class V_1
    {
        public const string URL = "https://api.iextrading.com/1.0/";

        public static string Chart(string _symbol, string range = "")
        {
            if (String.IsNullOrEmpty(range))
            {
                return LibraryStandard.HttpRequestUtility.GetRequest(URL + "stock/" + _symbol + "/chart");
            }
            else
            {
                return LibraryStandard.HttpRequestUtility.GetRequest(URL + "stock/" + _symbol + "/chart/" + range);
            }
        }

        public static string Symbols()
        {
            return LibraryStandard.HttpRequestUtility.GetRequest(URL + "ref-data/symbols");
        }
    }
}
