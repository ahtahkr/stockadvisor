using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AustinsFirstProject.AlphaVantage
{
    public static class TIME_SERIES_DAILY
    {
        private const string url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";

        public static string GET(string ticker, string apikey)
        {
            try
            {
                JObject abc = JObject.Parse(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(HttpRequestUtility.GetRequest(url + ticker + "&apikey=" + apikey))));
                List<JObject> cde = new List<JObject>();

                int count = 0;
                string[] _date;
                int fgh;

                foreach (JToken result in abc.Children())
                {
                    if (count > 0)
                    {
                        foreach (JToken result_two in result.Children())
                        {
                            foreach (JToken result_three in result_two.Children())
                            {
                                _date = result_three.ToString().Split('"');
                                JObject jObject = new JObject();
                                jObject.Add("_date", _date[1]);
                                jObject.Add("ticker", ticker);
                                
                                foreach (JToken result_four in result_three.Children())
                                {
                                    int _count = 0;
                                    foreach (JToken result_five in result_four.Children())
                                    {
                                        _date = result_five.ToString().Split(' ');
                                        _date = _date[2].Split('"');

                                        if (_count == 0)
                                        {
                                            jObject.Add("open", Convert.ToDecimal(_date[1]));
                                        } else if (_count == 1)
                                        {
                                            jObject.Add("high", Convert.ToDecimal(_date[1]));
                                        }
                                        else if (_count == 2)
                                        {
                                            jObject.Add("low", Convert.ToDecimal(_date[1]));
                                        }
                                        else if (_count == 3)
                                        {
                                            jObject.Add("close", Convert.ToDecimal(_date[1]));
                                        }
                                        else if (_count == 4)
                                        {
                                            jObject.Add("volume", Convert.ToInt32(_date[1]));
                                        }
                                        _count++;
                                    }
                                }
                                cde.Add(jObject);
                            }
                        }
                    }
                    count++;
                }
                return JsonConvert.SerializeObject(cde);
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
