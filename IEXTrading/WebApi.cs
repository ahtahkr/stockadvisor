using System;

namespace IEXTrading.WebApi
{
    public static class Api_1
    {
        private const string URI = "https://api.iextrading.com/1.0/";

        public static string Previous(string symbol = "market")
        {
            return Library.HttpRequestUtility.GetRequest(URI + "stock/" + symbol + "/previous");
        }
    }
}
