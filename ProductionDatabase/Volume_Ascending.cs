using Newtonsoft.Json;
using ProductionDatabase.Email;
using System;
using System.Collections.Generic;
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

        public void Get_from_Database(string connection_string, Dictionary<string, object> parameters = null)
        {
            string _va = "";
            try
            {
                _va = Library.Database.ExecuteProcedure.Get("[fsn].[Volume_Ascending]", connection_string, parameters);
                this.Volume_Ascendings =
                    JsonConvert.DeserializeObject<List<Volume_Ascending>>(_va);
            } catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname, "Database returned: " + _va, ex.Message);
            }
        }

        public string Get_Email_Body()
        {
            string body = Environment.NewLine + Environment.NewLine + Enum.GetName(typeof(Email_Types), (int) this.Email_Type) + Environment.NewLine;

            for (int a = 0; a < this.Volume_Ascendings.Count; a++)
            {
                body += this.Volume_Ascendings[a].Symbol + " - " + this.Volume_Ascendings[a].Url + " Volume_Change_Percentage: [" + this.Volume_Ascendings[a].Volume_change_percentage + "] - Latest Date: [" + this.Volume_Ascendings[a].Latest_date + "] [" + this.Volume_Ascendings[a].Latest_volume + "] Previous Date: [" + this.Volume_Ascendings[a].Previous_date + "] [" + this.Volume_Ascendings[a].Previous_volume + "]" + Environment.NewLine;
            }
            return body;
        }
    }

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
}
