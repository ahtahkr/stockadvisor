using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace IEXTrading
{
    public enum Web_Api_Version
    {
        One_point_Zero
    }
    public static class Operator
    {
        public static void Save_Previous_to_File(Web_Api_Version web_api_version, string folder, string symbol = "market")
        {
            string filename = "Share_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt";
            folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            folder = Path.Combine(folder, filename);
            string previous = "Before";

            if (web_api_version == Web_Api_Version.One_point_Zero)
            {
                previous = WebApi.Api_1.Previous(symbol);
                string _share;
                string[] _shares;

                List<ProductionDatabase.Share> shares = new List<ProductionDatabase.Share>();

                JObject o = JObject.Parse(previous);
                foreach (var c in o)
                {
                    _share = c.ToString();
                    _shares = _share.Split('{')[1].Split('}');

                    ProductionDatabase.Share pShare = new ProductionDatabase.Share();
                    Modal.Api_1.Share eShare 
                        = JsonConvert.DeserializeObject<Modal.Api_1.Share>('{' + _shares[0] + '}');

                    pShare.Symbol = eShare.Symbol;
                    pShare.Date = eShare.Date;
                    pShare.Change = eShare.Change;
                    pShare.ChangePercent = eShare.ChangePercent;
                    pShare.Close = eShare.Close;
                    pShare.High = eShare.High;
                    pShare.Low = eShare.Low;
                    pShare.Open = eShare.Open;
                    pShare.UnadjustedVolume = eShare.UnadjustedVolume;
                    pShare.Volume = eShare.Volume;
                    pShare.Vwap = eShare.Vwap;

                    shares.Add(pShare);
                }
                File.AppendAllText(
                     folder
                    , JsonConvert.SerializeObject(shares)
                );
            } else
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;

                Library.Logger.Log_Error(fullMethodName, "The web_api_version received: " + web_api_version);

                return;
            }
        }

        public static void Save_Chart_Range_to_File(Web_Api_Version web_api_version, string folder, string symbol, string range)
        {
            string filename = "Share_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt";
            folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            folder = Path.Combine(folder, filename);
            string previous = "Before";

            if (web_api_version == Web_Api_Version.One_point_Zero)
            {
                previous = WebApi.Api_1.Chart(symbol, range);

                List<Modal.Api_1.Share> eShares = JsonConvert.DeserializeObject<List<Modal.Api_1.Share>>(previous);
                List<ProductionDatabase.Share> pShares = new List<ProductionDatabase.Share>();
                symbol = symbol.ToUpper();

                for (int a = 0; a < eShares.Count; a++)
                {
                    ProductionDatabase.Share pShare = new ProductionDatabase.Share();
                    pShare.Symbol = symbol;
                    pShare.Date = eShares[a].Date;
                    pShare.Change = eShares[a].Change;
                    pShare.ChangePercent = eShares[a].ChangePercent;
                    pShare.Close = eShares[a].Close;
                    pShare.High = eShares[a].High;
                    pShare.Low = eShares[a].Low;
                    pShare.Open = eShares[a].Open;
                    pShare.UnadjustedVolume = eShares[a].UnadjustedVolume;
                    pShare.Volume = eShares[a].Volume;
                    pShare.Vwap = eShares[a].Vwap;

                    pShares.Add(pShare);
                }
                File.AppendAllText(
                 folder
                , JsonConvert.SerializeObject(pShares)
            );
            }
            else
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;

                Library.Logger.Log_Error(fullMethodName, "The web_api_version received: " + web_api_version);

                return;
            }            
        }
    }
}
