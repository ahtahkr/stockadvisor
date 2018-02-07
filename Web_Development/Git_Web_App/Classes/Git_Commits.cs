using AustinsFirstProject.Git_Web_App.Model;
using AustinsFirstProject.Library;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.Git_Web_App.Classes
{
    public class Git_File_Changes
    {
        public string File_FullName { get; set; }
        public int File_Status { get; set; }
        public string Patch { get; set; }
    }
    public class Git_Commit
    {
        private enum File_Status { Added=0, Modified=1, Renamed=2, Deleted=3}
        public Author Author { get; set; }
        public string Message { get; set; }
        public string MessageShort { get; set; }
        public string Sha { get; set; }

        public string Patch { get; set; }

        public string Repo_Path { get; set; }

        public List<Git_File_Changes> Git_File_Changes { get; set; }
        
        public Git_Commit()
        {
            this.Author = new Author();
            this.Git_File_Changes = new List<Git_File_Changes>();
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

        public void Set_File_Status_Patch(string file_name, string status, string patch)
        {
            try
            {
                Git_File_Changes git_f_c = new Git_File_Changes();
                git_f_c.File_FullName = file_name;
                git_f_c.File_Status = this.Get_Int_From_Enum(status);
                git_f_c.Patch = this.Get_Patch(patch);
                this.Git_File_Changes.Add(git_f_c);
            }
            catch (Exception ex)
            {
                Logger.Log_Error(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "  Error Msg: " + ex.Message, "File_Status");
            }
        }

        public string Get_Patch(string patch)
        {
            try
            {
                if (patch.Contains("@@"))
                {
                    return patch.Substring(
                        patch.IndexOf("@@")
                        , patch.Length - patch.IndexOf("@@")
                    );
                }
                else
                {
                    //Logger.Log_Error(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Patch returning as Binary File. Patch: " + patch);
                    //return patch;
                }
            } catch (Exception ex)
            {
                Logger.Log_Error(this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " Error Msg: " + ex.Message);
            }
            return patch;
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
                            
                            foreach (PatchEntryChanges ptc in patch)
                            {
                                this.Set_File_Status_Patch(ptc.Path, ptc.Status.ToString(),ptc.Patch);
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