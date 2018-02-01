using AustinsFirstProject.Library;
using AustinsFirstProject.Library_Git.Helpers;
using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AustinsFirstProject.Library_Git
{
    public static class GitAccessor
        {
            private static Credentials CreateUsernamePasswordCredentials(string user, string pass, bool secure)
            {
                if (secure)
                {
                    return new SecureUsernamePasswordCredentials
                    {
                        Username = user,
                        Password = Constants.StringToSecureString(pass),
                    };
                }

                return new UsernamePasswordCredentials
                {
                    Username = user,
                    Password = pass,
                };
            }
            private static bool TransferProgress(TransferProgress progress)
            {
                Console.WriteLine("TransferProgress: " + $"Objects: {progress.ReceivedObjects} of {progress.TotalObjects}, Bytes: {progress.ReceivedBytes}");
                return true;
            }
            public static Return Git_Clone(string git_url, string git_directory, string usr = "", string pass = "")
            {
                CloneOptions cloneOptions = new CloneOptions()
                {
                    OnTransferProgress = TransferProgress
                };

                if (!String.IsNullOrEmpty(usr) && !String.IsNullOrEmpty(pass))
                {
                    cloneOptions.CredentialsProvider = (_url, _user, _cred) => CreateUsernamePasswordCredentials(usr, pass, true);
                }

                if (String.IsNullOrEmpty(git_directory))
                {
                    git_directory = Path.Combine(Utility.Get_Directory(), Guid.NewGuid().ToString());
                }

                Return result;
                try
                {
                    result = new Return(0,
                        Repository.Clone(
                            git_url,
                            git_directory, cloneOptions)
                        );

                    Logger.Log("Git Clone success. Git_Url: " + git_url + " Git_Directory: " + git_directory + " U: " + usr + " P: " + pass, "Git_Clone");

                }
                catch (Exception ex)
                {
                    result = new Return(1, ex.Message);
                    Logger.Log_Error("Git Clone failed. Git_Url: " + git_url + " Git_Directory: " + git_directory + " U: " + usr + " P: " + pass + " Error Message: " + ex.Message, "Git_Clone");
                }
                return result;
            }

            public static void Git_Get_All_Commits(string git_dir)
            {
                using (Repository Git = new Repository(git_dir))
                {
                    //Logger.Log(JsonConvert.SerializeObject(Git), "Git");
                    int counter = 0;
                    Console.WriteLine("Git Commit Message.");
                    foreach (var Commit in Git.Commits)
                    {
                        if (counter == 0)
                        {
                            Logger.Log(JsonConvert.SerializeObject(Commit), "Commit");
                            Console.WriteLine("{0}",
                                JsonConvert.SerializeObject(Commit)
                            //   ,Commit.Id.ToString(7)
                            //   ,Commit.Author.Name
                            );
                            counter++;
                        }
                    }
                }

            }
        }
    }
