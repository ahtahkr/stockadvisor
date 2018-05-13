using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ProductionDatabase.Modal
{
    public class Vol_Asc_Next_Collection
    {
        public List<Vol_Asc_Next> Volume_Ascendings;

        public Vol_Asc_Next_Collection()
        {
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

            parameters.Add("_date", date.ToString("yyyy-MM-dd"));

            string _va = "";
            try
            {
                _va = Library.Database.ExecuteProcedure.Get("[th].[Vol_Asc_Next]", _connection_string, parameters);
                this.Volume_Ascendings =
                    JsonConvert.DeserializeObject<List<Vol_Asc_Next>>(_va);

            }
            catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname, "Database returned: " + _va, ex.Message);
            }
        }
    }

    public class Vol_Asc_Next
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

        public DateTime Next_Date { get; set; }
        public double Next_open { get; set; }
        public double Next_close { get; set; }
        public double Next_high { get; set; }
        public double Next_low { get; set; }
        public double Next_change { get; set; }
        public double Next_changePercentage { get; set; }
        public int Next_volume { get; set; }
        public double Next_vwap { get; set; }

        public string Url_Chart { get; set; }
        public string Url_Data { get; set; }
    }
}
