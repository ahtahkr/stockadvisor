using Newtonsoft.Json;
using ProductionDatabase.Email;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ProductionDatabase.Modal
{
    public class Volume_Ascending_Collection
    {
        public List<Vol_Asc> Volume_Ascendings;
        private Email_Types Email_Type;

        public Volume_Ascending_Collection()
        {
            this.Email_Type = Email_Types.Vol_Asc;
        }

        public void Get_from_Database(string _connection_string, DateTime date, int? _minimum_change = null, int? _minimum_volume = null, int? _maximum_close_price = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (_minimum_change != null && (_minimum_change >= 0 || _minimum_change <= 0))
            {
                parameters.Add("Minimum_Change", _minimum_change);
            }

            if (_minimum_volume != null && (_minimum_volume >= 0 || _minimum_volume <= 0))
            {
                parameters.Add("Volume", _minimum_volume);
            }

            if (_maximum_close_price != null && (_maximum_close_price >= 0 || _maximum_close_price <= 0))
            {
                parameters.Add("Max_Close_Price", _maximum_close_price);
            }

            parameters.Add("_date",date.ToString("yyyy-MM-dd"));

            string _va = "";
            try
            {
                _va = Library.Database.ExecuteProcedure.Get("[th].[Vol_Asc]", _connection_string, parameters);
                this.Volume_Ascendings =
                    JsonConvert.DeserializeObject<List<Vol_Asc>>(_va);
                this.Volume_Ascendings.ForEach(vol => vol.Calculate());

            } catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname, "Database returned: " + _va, ex.Message);
            }
        }

        public string Get_Email_Html_Body()
        {
            string body = "<h4>" + this.GetType().Name + "</h4>";
            body += "<table style=\"border: 1px solid black;\"><tr style=\"border: 1px solid black;\"><th style=\"border: 1px solid black;\">";

            List<string> cols = Utility.Get_Class_Field_Names(this.Volume_Ascendings[0]);
            body += cols.Aggregate((i, j) => i + "</th><th style=\"border: 1px solid black;\">" + j);
            body += "</th></tr>";

            List<string> vals;

            for (int a = 0; a < this.Volume_Ascendings.Count; a++)
            {
                vals = Utility.Get_Class_Field_Values(this.Volume_Ascendings[a]);
                for (int b = 0; b < vals.Count; b++)
                {
                    if (vals[b].Length > 4 && vals[b].Substring(0, 4).Equals("http"))
                    {
                        vals[b] = "<a href=\""+ vals[b] + "\" target=\"_blank\">" + vals[b] + "</a>";
                    }
                }

                body += "<tr style=\"border: 1px solid black;\">";
                body += "<td style=\"border: 1px solid black;\">" + vals.Aggregate((i, j) => i + "</td><td style=\"border: 1px solid black;\">" + j) + "</td></tr>";
            }
            body += "</table>";
            return body;
        }
    }

    public class Vol_Asc
    {
        public string Symbol { get; set; }
        public DateTime Previous_date { get; set; }
        public double Previous_open { get; set; }
        public double Previous_close { get; set; }
        public double Previous_high { get; set; }
        public double Previous_low { get; set; }
        public double Previous_change { get; set; }
        public double Previous_changePercentage { get; set; }
        public int Previous_volume { get; set; }
        public double Previous_vwap { get; set; }
        public DateTime Mark_date { get; set; }
        public double Mark_open { get; set; }
        public double Mark_close { get; set; }
        public double Mark_high { get; set; }
        public double Mark_low { get; set; }
        public double Mark_change { get; set; }
        public double Mark_changePercentage { get; set; }
        public int Mark_volume { get; set; }
        public double Mark_vwap { get; set; }
        public string Url_Chart { get; set; }
        public string Url_Data { get; set; }

        public double Volume_Change_Percentage { get; set; }

        public void Calculate()
        {
            if (this.Previous_volume == 0) { return; }
            this.Volume_Change_Percentage = ((this.Mark_volume - this.Previous_volume) / this.Previous_volume) * 100;
        }
    }
}
