using AustinsFirstProject.Library;
using AustinsFirstProject.StockAdvisor.IEXTrading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void Upload_Files(object sender = null, ElapsedEventArgs e = null)
        {
            string directory = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Files_dir"]);

            string process_dir = Path.Combine(directory, "Processing");
            if (!Directory.Exists(process_dir)) { Directory.CreateDirectory(process_dir); }
            string error = Path.Combine(process_dir, "Error");
            if (!Directory.Exists(error)) { Directory.CreateDirectory(error); }

            if (Directory.Exists(directory))
            {
                string file = AustinsFirstProject.Library.Utility.FileUtility.GetFile(directory);

                if (File.Exists(file))
                {
                    string filename = Path.GetFileName(file);
                    string[] filename_ = filename.Split('_');

                    if (filename_[0].Equals("Symbol") || filename_[0].Equals("ShareDetail"))
                    {                      
                        string jsondata = File.ReadAllText(file);

                        if (String.IsNullOrEmpty(jsondata))
                        {
                            Logger.Log_Error("The file " + file + " is empty.");
                            File.Move(file, Path.Combine(error, filename));
                        } else
                        {
                            File.Delete(file);

                            if (filename_[0].Equals("Symbol"))
                            {
                                try
                                {
                                    List<Symbol_> symbols = new List<Symbol_>();
                                    symbols = JsonConvert.DeserializeObject<List<Symbol_>>(jsondata);

                                    for (int a = 0; a < symbols.Count; a++)
                                    {
                                        symbols[a].Save_in_Database();
                                    }
                                } catch (Exception ex)
                                {
                                    Logger.Log_Error("Converting " + jsondata + " to List<Symbol> failed. Error Msg: " + ex.Message);
                                }
                            }
                        }
                    } else
                    {
                        Logger.Log_Error(file + " received from AustinsFirstProject.Library.Utility.FileUtility.GetFile will not be processed.");
                    }
                } else
                {
                    Logger.Log_Error(file + " received from AustinsFirstProject.Library.Utility.FileUtility.GetFile is invalid.");
                }
            } else
            {
                Logger.Log_Error("The IEXTrading_Files_dir in App.config file is not a valid directory. IEXTrading_Files_dir = " + directory);
            }
        }
    }
}
