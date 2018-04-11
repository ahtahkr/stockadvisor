using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IEXTrading.WebApi.Modal.Api_1
{
    public class Market
    {
        public List<Share> Shares;
        public Market()
        {
            this.Shares = new List<Share>();
        }
        public bool Process_Info(string _market)
        {
            try
            {
                string _share;
                string[] _shares;

                JObject o = JObject.Parse(_market);
                foreach (var c in o)
                {
                    _share = c.ToString();
                    _shares = _share.Split('{')[1].Split('}');
                    this.Shares.Add(
                        JsonConvert.DeserializeObject<Share>('{' + _shares[0] + '}'));
                }
                return true;
            }
            catch (Exception ex)
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;

                Library.Logger.Log_Error(fullMethodName, "_market: " + _market, ex.Message);
                return false;
            }
        }
        public void Save_in_Database(string connection_string)
        {
            for (int a = 0; a < this.Shares.Count; a++)
            {
                this.Shares[a].Save_in_Database(connection_string);
            }
        }
    }
}

namespace IEXTrading.WebApi.Modal
{
    public class Share
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

        public string Save_in_Database(string connection_string)
        {
            Dictionary<string, object> parameters
                = new Dictionary<string, object>
                {
                    { "symbol", this.Symbol },
                    { "date", this.Date },
                    { "open", this.Open },
                    { "high", this.High },
                    { "low", this.Low },
                    { "close", this.Close },
                    { "volume", this.Volume },
                    { "change", this.Change },
                    { "changePercent", this.ChangePercent },
                    { "vwap", this.Vwap }
                };

            return Library.Database.ExecuteProcedure.Get(
                "[fsn].[Share_Insert_Update]",
                connection_string,
                parameters
            );
        }
    }
}