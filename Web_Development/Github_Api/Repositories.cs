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
        private const string APP_NAME = "GIT_INFO_ACCESSOR";
        private const string GitHub_Api_Token = "f1a471df899699b74ad6cbc021b5cb6d9a963908";

        public static class Repositories
        {
            /// <summary>
            /// https://api.github.com/repos/{owner}/{repo}
            /// </summary>
            public static string Get_Basic_Info(string _owner, string _repo, string token = "")
            {
                string result;
                try
                {
                    var client = new GitHubClient(new ProductHeaderValue(APP_NAME));

                    if (!String.IsNullOrEmpty(token))
                    {
                        var tokenAuth = new Octokit.Credentials(token);
                        client.Credentials = tokenAuth;
                    }
                    result = JsonConvert.SerializeObject(client.Repository.Get(_owner, _repo));
                    dynamic dynamic = JObject.Parse(result);
                    result = dynamic.Result.ToString();

                } catch (Exception ex)
                {
                    Logger.Log_Error("AustinsFirstProject.Github_Api.Api.Rest_Api_V3.Repositories.Get_Basic_Info failed. Error Msg: " + ex.Message);
                    result = "";
                }
                return result;
            }

            public static string Get_Commits(string _owner, string _repo, string token = "")
            {
                string result;
                try
                {
                    var client = new GitHubClient(new ProductHeaderValue(APP_NAME));

                    if (!String.IsNullOrEmpty(token))
                    {
                        var tokenAuth = new Octokit.Credentials(token);
                        client.Credentials = tokenAuth;
                    }
                    result = JsonConvert.SerializeObject(client.Repository.Commit.GetAll(_owner, _repo));
                    dynamic dynamic = JObject.Parse(result);
                    result = dynamic.Result.ToString();                    
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("AustinsFirstProject.Github_Api.Api.Rest_Api_V3.Repositories.Get_Commits failed. Error Msg: " + ex.Message);
                    result = "";
                }
                return result;
            }
        }
    }
}
