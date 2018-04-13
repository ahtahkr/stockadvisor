using Newtonsoft.Json;
using ProductionDatabase.Email;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProductionDatabase.Email
{
    enum Email_Types { Volume_Ascending }
}
namespace ProductionDatabase.Modal
{
    public class Volume_Ascending_Collection
    {
        private List<Volume_Ascending> Volume_Ascendings;
        private Email_Types Email_Type;

        public Volume_Ascending_Collection()
        {
            this.Email_Type = Email_Types.Volume_Ascending;
        }

        public void Get_from_Database(string _connection_string, int? _minimum_change = null, int? _minimum_volume = null, int? _maximum_close_price = null )
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

            string _va = "";
            try
            {
                _va = Library.Database.ExecuteProcedure.Get("[fsn].[Volume_Ascending]", _connection_string, parameters);
                this.Volume_Ascendings =
                    JsonConvert.DeserializeObject<List<Volume_Ascending>>(_va);
            } catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname, "Database returned: " + _va, ex.Message);
            }
        }

        private DataTable Get_Table()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Symbol", typeof(string));
            table.Columns.Add("Url", typeof(string));
            table.Columns.Add("Latest_Date", typeof(DateTime));
            table.Columns.Add("Previous_Date", typeof(DateTime));
            table.Columns.Add("Latest_Volume", typeof(int));
            table.Columns.Add("Previous_Volume", typeof(int));
            table.Columns.Add("Volume_Change_Percentage", typeof(int));

            for (int a = 0; a< this.Volume_Ascendings.Count; a++)
            {
                table.Rows.Add(
                    this.Volume_Ascendings[a].Symbol, this.Volume_Ascendings[a].Url, this.Volume_Ascendings[a].Latest_Date, this.Volume_Ascendings[a].Previous_Date
                    , this.Volume_Ascendings[a].Latest_Volume, this.Volume_Ascendings[a].Previous_Volume, this.Volume_Ascendings[a].Volume_Change_Percentage
                );
            }

            return table;
        }

        public string Get_Email_Body()
        {
            string body = Environment.NewLine + Environment.NewLine 
                            + Enum.GetName(typeof(Email_Types), (int) this.Email_Type) + Environment.NewLine;

            body += Library.Table.Convert_to_CSV_String(this.Get_Table());
            return body;
        }
    }

    public class Volume_Ascending
    {
        public string Symbol { get; set; }
        public DateTime Latest_Date { get; set; }
        public DateTime Previous_Date { get; set; }
        public int Latest_Volume { get; set; }
        public int Previous_Volume { get; set; }
        public double Volume_Change_Percentage { get; set; }
        public string Url { get; set; }
    }
}
