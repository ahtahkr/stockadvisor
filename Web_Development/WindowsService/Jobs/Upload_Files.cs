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
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string _filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Upload_Files.log");

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            string go_ahead = Convert.ToString(ConfigurationManager.AppSettings["Upload_Files"]);

            File.WriteAllText(_filename, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + " : Upload_Files. " + go_ahead); 
            
            if (!go_ahead.Equals("true"))
            {
                return;
            }

            string directory = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Files_dir"]);

            string process_dir = Path.Combine(directory, "Processing");
            if (!Directory.Exists(process_dir)) { Directory.CreateDirectory(process_dir); }
            string error = Path.Combine(process_dir, "Error");
            if (!Directory.Exists(error)) { Directory.CreateDirectory(error); }

            if (Directory.Exists(directory))
            {
                string file = AustinsFirstProject.Library.Utility.FileUtility.GetFile(directory);

                if (String.IsNullOrEmpty(file)) { }
                else
                {
                    if (File.Exists(file))
                    {
                        string filename = Path.GetFileName(file);
                        string[] filename_ = filename.Split('_');

                        if (filename_[0].Equals("Symbol") || filename_[0].Equals("ShareDetail") || filename_[0].Equals("Last") || filename_[0].Equals("News"))
                        {
                            string jsondata = File.ReadAllText(file);

                            if (String.IsNullOrEmpty(jsondata))
                            {
                                Logger.Log_Error("Windows Service. Upload Files. The file " + file + " is empty.");
                                File.Move(file, Path.Combine(error, filename));
                            }
                            else
                            {
                                File.Delete(file);

                                if (filename_[0].Equals("Symbol"))
                                {
                                    List<Symbol_> unsuccessful_symbols = new List<Symbol_>();
                                    try
                                    {
                                        List<Symbol_> symbols = new List<Symbol_>();
                                        symbols = JsonConvert.DeserializeObject<List<Symbol_>>(jsondata);

                                        for (int a = 0; a < symbols.Count; a++)
                                        {
                                            if (symbols[a].Save_in_Database() != 0)
                                            {
                                                unsuccessful_symbols.Add(symbols[a]);
                                            }
                                        }

                                        if (unsuccessful_symbols.Count > 0)
                                        {
                                            File.WriteAllText(Path.Combine(error, filename), JsonConvert.SerializeObject(unsuccessful_symbols));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        File.WriteAllText(Path.Combine(error, filename), jsondata);
                                        Logger.Log_Error("Windows Service. Upload Files. Converting " + jsondata + " to List<Symbol_> failed. Error Msg: " + ex.Message);
                                    }
                                }
                                else if (filename_[0].Equals("ShareDetail"))
                                {
                                    List<ShareDetail> unsuccessful_sharedetails = new List<ShareDetail>();
                                    try
                                    {
                                        List<ShareDetail> sharedetails = new List<ShareDetail>();
                                        sharedetails = JsonConvert.DeserializeObject<List<ShareDetail>>(jsondata);

                                        for (int a = 0; a < sharedetails.Count; a++)
                                        {
                                            if (sharedetails[a].Save_in_Database() != 0)
                                            {
                                                unsuccessful_sharedetails.Add(sharedetails[a]);
                                            }
                                        }
                                        if (unsuccessful_sharedetails.Count > 0)
                                        {
                                            File.WriteAllText(Path.Combine(error, filename), JsonConvert.SerializeObject(unsuccessful_sharedetails));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        File.WriteAllText(Path.Combine(error, filename), jsondata);
                                        Logger.Log_Error("Windows Service. Upload Files. Converting " + jsondata + " to List<ShareDetail> failed. Error Msg: " + ex.Message);
                                    }
                                } else if (filename_[0].Equals("Last"))
                                {
                                    List<Last> unsuccessful_lasts = new List<Last>();
                                    try
                                    {
                                        List<Last> lasts = new List<Last>();
                                        lasts = JsonConvert.DeserializeObject<List<Last>>(jsondata);

                                        for (int a = 0; a < lasts.Count; a++)
                                        {
                                            if (lasts[a].Save_in_Database() != 0)
                                            {
                                                unsuccessful_lasts.Add(lasts[a]);
                                            }
                                        }

                                        if (unsuccessful_lasts.Count > 0)
                                        {
                                            File.WriteAllText(Path.Combine(error, filename), JsonConvert.SerializeObject(unsuccessful_lasts));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        File.WriteAllText(Path.Combine(error, filename), jsondata);
                                        Logger.Log_Error("Windows Service. Upload Files. Converting " + jsondata + " to List<Last> failed. Error Msg: " + ex.Message);
                                    }
                                }
                                else if (filename_[0].Equals("News"))
                                {
                                    List<Gen> unsuccessful_Gen = new List<Gen>();
                                    try
                                    {
                                        List<Gen> Gen = new List<Gen>();
                                        Gen = JsonConvert.DeserializeObject<List<Gen>>(jsondata);

                                        for (int a = 0; a < Gen.Count; a++)
                                        {
                                            if (Gen[a].Save_in_Database() != 0)
                                            {
                                                unsuccessful_Gen.Add(Gen[a]);
                                            }
                                        }

                                        if (unsuccessful_Gen.Count > 0)
                                        {
                                            File.WriteAllText(Path.Combine(error, filename), JsonConvert.SerializeObject(unsuccessful_Gen));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        File.WriteAllText(Path.Combine(error, filename), jsondata);
                                        Logger.Log_Error("Windows Service. Upload Files. Converting " + jsondata + " to List<Gen> failed. Error Msg: " + ex.Message);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Logger.Log_Error(file + " received from AustinsFirstProject.Library.Utility.FileUtility.GetFile will not be processed.");
                            File.Move(file, Path.Combine(error, filename));
                        }
                    }
                    else
                    {
                        Logger.Log_Error(file + " received from AustinsFirstProject.Library.Utility.FileUtility.GetFile does not exist.");
                    }
                }
            } else
            {
                Logger.Log_Error("Windows Service. Upload Files. The IEXTrading_Files_dir in App.config file is not a valid directory. IEXTrading_Files_dir = " + directory);
            }
        }
    }
}
