using System;
using AustinsFirstProject.CoreLibrary.Database;
using AustinsFirstProject.Library_Git;
using Newtonsoft.Json;
using LibGit2Sharp;
using System.Collections;
using System.Collections.Generic;
using AustinsFirstProject.Library;
using AustinsFirstProject.StockAdvisor.IEXTrading;
using Newtonsoft.Json.Linq;
using System.IO;

namespace AustinsFirstProject.Tester
{
    class Program
    {
        enum File_Status { Added = 0, Modified = 1, Deleted = 2, Unknown = 3 }

        static void Main(string[] args)
        {
            //Symbols.Download_Symbols();
            string connection_string = "Data Source=AADHIKARI10\\SQLEXPRESS;Initial Catalog=austin_stock_processor;User ID=developer;Password=developer";
            connection_string = "Data Source=DESKTOP-BJ0AH8G;Initial Catalog=austin_stock_processor;User ID=ashadhik;Password=ashadhik;";


            Lasts lasts = new Lasts();
            if (lasts.Call_Api())
            {
                lasts.Save_In_File();
            }

            /*
                string git_url = "https://asadhikari@bitbucket.org/austinsfirstproject/asp-net-core.git";
                string username = "as.adhikari@outlook.com";
                string password = "Falgunfeb11!";
                Return re = GitAccessor.Git_Clone(git_url, "", username, password);
                Console.WriteLine(JsonConvert.SerializeObject(re));
                
            */
            //GitAccessor.Git_Get_All_Commits(@"F:\features\austin_first_project\.git");
            //Library.Return _return = SQLiteDB.Database.Create_Database();

            //Console.WriteLine(JsonConvert.SerializeObject(_return));

            /*
            string repository = "F:\\Kenall_TekLink_Source_Code\\.git";
            if (Repository.IsValid(repository))
            {
                Repository repo = new Repository(repository);

                Commit commit = repo.Lookup<Commit>("36ef4e35c02a7e8d7b760c0b6920ecd8c2121d50");

                if (commit != null)
                {
                    Tree commit_tree = commit.Tree;

                    IEnumerator<Commit> commit_iterator = commit.Parents.GetEnumerator();
                    if (commit_iterator.MoveNext())
                    {
                        Tree parent_tree = commit_iterator.Current.Tree;

                        Patch patch = repo.Diff.Compare<Patch>(parent_tree, commit_tree);
                        foreach (PatchEntryChanges ptc in patch)
                        {
                            Console.WriteLine("Here");
                            Console.WriteLine(JsonConvert.SerializeObject(ptc));
                            Console.WriteLine(ptc.Status + " -> " + ptc.Path);
                            Logger.Log(ptc.Patch, "patch");
                            Console.ReadLine();
                        }
                    }
                }
            }

            Console.ReadLine();
            */
        }

        

        private static void Get_All_Shares()
        {
            Shares shares = new Shares
            {
                Database_Connection_String = "Data Source=AADHIKARI10\\SQLEXPRESS;Initial Catalog=austin_stock_processor;Persist Security Info=True;User ID=developer;Password=developer"
            };
            shares.Get_Ticker("VCOR");
            Console.WriteLine(shares.Shares_Date_Close());
            Console.ReadLine();
        }

        private static void Get_Companies()
        {
            Console.WriteLine("Retrieving Companies from 'api.intrinio.com'.");

            string url = "https://api.intrinio.com/companies?page_number=";
            string username = "7d11f2289bbb035fc56c6ff5b654e6bd";
            string password = "6c9a7fdd82022fcea63c2367a117750b";

            int a = 1;
            string result = HttpRequestUtility.GetRequest(url + a, username, password);

            Console.WriteLine(result);
            //Companies _companies = JsonConvert.DeserializeObject<Companies>(result);
            //_companies.Save_in_Database();

            //int total_pages = _companies.total_pages;

            //for (a = 2; a <= total_pages; a++)
            //{
            //    result = HttpRequestUtility.GetRequest(url + a, username, password);
            //    _companies = JsonConvert.DeserializeObject<Companies>(result);
            //    _companies.Save_in_Database();
            //}

            Console.WriteLine("Complete.");
            Console.ReadLine();
        }
    }
}
