using AustinsFirstProject.Git_Web_App.Model;
using AustinsFirstProject.Library;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Classes
{
    public class Git_Commit
    {
        private enum File_Status { Added=0, Modified=1, Renamed=2, Deleted=3}
        public Author Author { get; set; }
        public string Message { get; set; }
        public string MessageShort { get; set; }
        public string Sha { get; set; }

        public string Repo_Path { get; set; }

        public Dictionary<string, int> Files_and_Status { get; set; }

        public Git_Commit()
        {
            this.Author = new Author();
            this.Files_and_Status = new Dictionary<string, int>();
        }        

        private int Get_Int_From_Enum(string key)
        {
            try
            {
                return Convert.ToInt16(Enum.Parse(typeof(File_Status), key));
                
                //if (Enum.IsDefined(typeof(Colors), colorValue) | colorValue.ToString().Contains(","))
                //    Console.WriteLine("Converted '{0}' to {1}.", colorString, colorValue.ToString());
                //else
                //    Console.WriteLine("{0} is not an underlying value of the Colors enumeration.", colorString);
            }
            catch (ArgumentException)
            {
                Logger.Log_Error("Invalid key [" + key + "] sent to Get_Int_From_Enum.");
                return 3;
            }
        }

        public string Get_String_From_Enum(int key)
        {
            try
            {
                return Enum.Parse(typeof(File_Status), key.ToString()).ToString();

                //if (Enum.IsDefined(typeof(Colors), colorValue) | colorValue.ToString().Contains(","))
                //    Console.WriteLine("Converted '{0}' to {1}.", colorString, colorValue.ToString());
                //else
                //    Console.WriteLine("{0} is not an underlying value of the Colors enumeration.", colorString);
            }
            catch (ArgumentException)
            {
                Logger.Log_Error("Invalid key [" + key + "] sent to Get_String_From_Enum.");
                return "Unknown";
            }
        }

        public void Set_Message(string msg = "", string msg_short = "")
        {
            if (String.IsNullOrEmpty(msg) && String.IsNullOrEmpty(msg_short))
            {
                return;
            } else
            {
                this.Message = msg;
                this.MessageShort = msg_short;
            }
        }

        public void Set_Author(Signature signature)
        {
            this.Author.Email = signature.Email;
            this.Author.Name = signature.Name;
            this.Author.When = signature.When;
        }

        public void Set_File_and_Status(string file_name, string status)
        {
            Logger.Log(file_name + " : " + status, "File_Status");
            try
            {
                this.Files_and_Status.Add(
                        file_name
                        , this.Get_Int_From_Enum(status)
                    );
            }
            catch (Exception ex)
            {
                Logger.Log_Error(file_name + " : " + status + "  Error Msg: " + ex.Message, "File_Status");
            }
        }

        public bool Set_Files_and_Status()
        {
            if (Repository.IsValid(this.Repo_Path))
            {
                Repository repo = new Repository(this.Repo_Path);

                if (!String.IsNullOrEmpty(this.Sha))
                {
                    Commit commit = repo.Lookup<Commit>(this.Sha);

                    if (commit != null)
                    {
                        Tree commit_tree = commit.Tree;
                        IEnumerator<Commit> commit_iterator = commit.Parents.GetEnumerator();
                        if (commit_iterator.MoveNext())
                        {
                            Tree parent_tree = commit_iterator.Current.Tree;

                            Patch patch = repo.Diff.Compare<Patch>(parent_tree, commit_tree);

                            Logger.Log("Here","adhikari");
                            foreach (PatchEntryChanges ptc in patch)
                            {
                                Logger.Log(ptc.Path + " : " + ptc.Status.ToString(), "adhikari");
                                this.Set_File_and_Status(ptc.Path, ptc.Status.ToString());
                            }
                            return true;
                        }
                        else
                        {
                            Logger.Log_Error("Move Next is false in " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                        }
                    } else
                    {
                        Logger.Log_Error("Commit is null in " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + ". Sha: " + this.Sha + ". Commit: " + commit.MessageShort );
                    }
                }
                else
                {
                    Logger.Log_Error("Repo_path varable = [" + this.Sha + "] in " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
            } else
            {
                Logger.Log_Error("Invalid Repo_path varable = [" + this.Repo_Path + "] in " + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
    }

    public class Git_Commits
    {
        public List<Git_Commit> Git__Commits { get; set; }

        public static Git_Commit Active;        

        public Git_Commits()
        {
            this.Git__Commits = new List<Git_Commit>();
        }
    }
}