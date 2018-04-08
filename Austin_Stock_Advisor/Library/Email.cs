using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustinStockAdvisor.Library
{
    enum Email_Types { Change_Ascending, Change_Decreased }

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
