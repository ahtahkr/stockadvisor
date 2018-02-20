using AustinsFirstProject.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Collections.Generic;

namespace AustinsFirstProject.Github_Api.Api
{
    public static class Rest_Api_V3
    {
        private const string API_URL = "https://api.github.com";

        public static class Repositories
        {
            /// <summary>
            /// https://api.github.com/repos/{owner}/{repo}
            /// </summary>
            public static string Get_Basic_Info(string app_name, string _owner, string _repo, string token)
            {
                string result;
                try
                {
                    var client = new GitHubClient(new ProductHeaderValue(app_name));

                    if (!String.IsNullOrEmpty(token))
                    {
                        var tokenAuth = new Octokit.Credentials(token);
                        client.Credentials = tokenAuth;
                    }
                    Octokit.Repository repository = client.Repository.Get(_owner, _repo).Result;
                    result = JsonConvert.SerializeObject(repository);
                } catch (Exception ex)
                {
                    Logger.Log_Error("AustinsFirstProject.Github_Api.Api.Rest_Api_V3.Repositories.Get_Basic_Info failed. Error Msg: " + ex.Message);
                    result = "";
                }
                return result;
            }

            public static string Get_Commits(string app_name, string _owner, string _repo, string token)
            {
                string result;
                try
                {
                    var client = new GitHubClient(new ProductHeaderValue(app_name));

                    if (!String.IsNullOrEmpty(token))
                    {
                        var tokenAuth = new Octokit.Credentials(token);
                        client.Credentials = tokenAuth;
                    }
                    IReadOnlyList<Octokit.GitHubCommit> commits = client.Repository.Commit.GetAll(_owner, _repo).Result;
                    result = JsonConvert.SerializeObject(commits);                    
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("AustinsFirstProject.Github_Api.Api.Rest_Api_V3.Repositories.Get_Commits failed. Error Msg: " + ex.Message);
                    result = "";
                }
                return result;
            }

            public static string Get_Commits(string app_name, int repo_id, string token)
            {
                string result;
                try
                {
                    var client = new GitHubClient(new ProductHeaderValue(app_name));

                    if (!String.IsNullOrEmpty(token))
                    {
                        var tokenAuth = new Octokit.Credentials(token);
                        client.Credentials = tokenAuth;
                    }
                    IReadOnlyList<Octokit.GitHubCommit> commits = client.Repository.Commit.GetAll(repo_id).Result;
                    result = JsonConvert.SerializeObject(commits);
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("AustinsFirstProject.Github_Api.Api.Rest_Api_V3.Repositories.Get_Commits_RepoId failed. Error Msg: " + ex.Message);
                    result = "";
                }
                return result;
            }

            public static string Get_Files_in_a_Commit(string app_name, int repo_id, string sha, string token)
            {
                string result;
                try
                {
                    var client = new GitHubClient(new ProductHeaderValue(app_name));

                    if (!String.IsNullOrEmpty(token))
                    {
                        var tokenAuth = new Octokit.Credentials(token);
                        client.Credentials = tokenAuth;
                    }
                    IReadOnlyList<Octokit.GitHubCommitFile> files = client.Repository.Commit.Get(repo_id, sha).Result.Files;
                    result = JsonConvert.SerializeObject(files);
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("AustinsFirstProject.Github_Api.Api.Rest_Api_V3.Repositories.Get_Commit failed. Error Msg: " + ex.Message);
                    result = "";
                }
                return result;
            }
        }
    }
}
