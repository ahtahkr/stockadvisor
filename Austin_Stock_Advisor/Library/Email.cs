using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustinStockAdvisor.Library
{
    enum Email_Types { Change_Ascending, Change_Decreased, Volume_Ascending }

    public class Volume_Ascending
    {
        public string Symbol { get; set; }
        public DateTime Latest_date { get; set; }
        public DateTime Previous_date { get; set; }
        public int Latest_volume { get; set; }
        public int Previous_volume { get; set; }
        public double Volume_change_percentage { get; set; }
        public string Url { get; set; }
    }
    public class Volume_Ascending_Email
    {
        private int Email_Type;
        public List<Volume_Ascending> Volume_Ascending { get; set; }

        public string Get_Email_Body()
        {
            string body = Environment.NewLine + Environment.NewLine + Enum.GetName(typeof(Email_Types), this.Email_Type) + Environment.NewLine;

            for (int a = 0; a < this.Volume_Ascending.Count; a++)
            {
                body += this.Volume_Ascending[a].Symbol + " - " + this.Volume_Ascending[a].Url + " Volume_Change_Percentage: ["+ this.Volume_Ascending[a].Volume_change_percentage +"] - Latest Date: [" + this.Volume_Ascending[a].Latest_date + "] [" + this.Volume_Ascending[a].Latest_volume + "] Previous Date: [" + this.Volume_Ascending[a].Previous_date + "] [" + this.Volume_Ascending[a].Previous_volume + "]" + Environment.NewLine;
            }
            return body;
        }

        public Volume_Ascending_Email()
        {
            Email_Type = (int)Email_Types.Volume_Ascending;

        }

        public void Get_Volume_Ascending_from_Database(string connection_string, Dictionary<string, object> param = null)
        {
            if (param == null || param.Count == 0)
            {
                string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                        "[fsn].[Volume_Ascending]"
                        , connection_string);
                try
                {
                    this.Volume_Ascending = JsonConvert.DeserializeObject<List<Volume_Ascending>>(companies);
                } catch (Exception ex)
                {
                    /* MethodFullName. */
                    string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                    Logger.Log_Error(methodfullname + " : " + ex.Message);
                    this.Volume_Ascending = new List<Library.Volume_Ascending>();
                }
            }
        }

    }

    public class Change_Decreased
    {
        public string Symbol { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public int Volume { get; set; }
    }

    public class Change_Decreased_Email
    {
        private int Email_Type;
        public List<Change_Decreased> Change_Decreased { get; set; }
        public Change_Decreased_Email()
        {
            Email_Type = (int)Email_Types.Change_Decreased;

        }

        public string Get_Email_Body()
        {
            string body = Environment.NewLine + Environment.NewLine + Enum.GetName(typeof(Email_Types), this.Email_Type) + Environment.NewLine;

            for (int a = 0; a < this.Change_Decreased.Count; a++)
            {
                body += this.Change_Decreased[a].Symbol + " - " + this.Change_Decreased[a].Url + " - Details: " + JsonConvert.SerializeObject(this.Change_Decreased[a]) + Environment.NewLine;
            }
            return body;
        }

    }
    
    public class Change_Ascending_Email
    {
        private int Email_Type;
        public List<Change_Ascending> Change_Ascending { get; set; }
        public Change_Ascending_Email()
        {
            Email_Type = (int) Email_Types.Change_Ascending;

        }

        public string Get_Email_Body()
        {
            string body = Environment.NewLine + Environment.NewLine + Enum.GetName(typeof(Email_Types), this.Email_Type) + Environment.NewLine;

            for (int a = 0; a < this.Change_Ascending.Count; a++)
            {
                body += this.Change_Ascending[a].Symbol + " - " + this.Change_Ascending[a].Url + " - WeeksToGoBack: " + this.Change_Ascending[a].WeeksToGoBack + " - MaxHigh: " + this.Change_Ascending[a].Max_High + " - Avg_Volume: " + this.Change_Ascending[a].AVG_Volume + Environment.NewLine;
            }
            return body;
        }
    }

    public class Change_Ascending
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int WeeksToGoBack { get; set; }
        public int Max_High { get; set; }
        public int AVG_Volume { get; set; }
        public string Url { get; set; }
    }
}
