using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.WebApplication.Areas.Robinhood.Models
{
    public class ShareCanvas
    {
        public Dictionary<SymbolInfo, List<ShareInfo>> Canvas { get; set; }

        public bool Check_Canvas_For_Symbol(string Symbol)
        {
            foreach(SymbolInfo item in this.Canvas.Keys)
            {
                if  (item.Symbol == Symbol)
                {
                    return true;
                }
            }
            return false;
        }

        public void Set_ShareDetail(List<Models.ShareDetail> sharedetail)
        {
            this.Canvas = new Dictionary<SymbolInfo, List<ShareInfo>>();

            for (int a = 0; a < sharedetail.Count; a++)
            {
                if (!Check_Canvas_For_Symbol(sharedetail[a].Symbol))
                {
                    SymbolInfo symbolInfo = new SymbolInfo();
                    symbolInfo.Symbol = sharedetail[a].Symbol;
                    symbolInfo.Name = sharedetail[a].Name;
                    symbolInfo.Type = sharedetail[a].Type;

                    List<ShareInfo> shareInfoList = new List<ShareInfo>();
                    for (int b = 0; b < sharedetail.Count; b++)
                    {
                        if (sharedetail[b].Symbol == symbolInfo.Symbol)
                        {
                            ShareInfo shareInfo = new ShareInfo();
                            shareInfo.Symbol = symbolInfo.Symbol;
                            shareInfo.Date = Convert.ToInt64(sharedetail[b].Date.ToUniversalTime().Subtract(
                                                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                                ).TotalMilliseconds);
                            shareInfo.Close = sharedetail[b].Close;
                            shareInfoList.Add(shareInfo);
                        }
                    }

                    this.Canvas.Add(symbolInfo, shareInfoList);
                }
            }
        }
    }

    public class SymbolInfo
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class ShareInfo
    {
        public string Symbol { get; set; } = "";
        public long Date { get; set; }
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
