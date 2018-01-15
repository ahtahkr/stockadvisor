   using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Library
{
    class HttpRequestUtility
    {
        public string GetRequest(String uri, string username = "", string password = "")
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "GET";
            httpWebRequest.MaximumAutomaticRedirections = 3;
            httpWebRequest.Timeout = 5000;

            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
            { 
                httpWebRequest.Credentials = new NetworkCredential("Mehrdad", "Password");
            }

            Console.WriteLine("Sending HTTP Request");
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var responseStream = httpWebResponse.GetResponseStream();
            string response = "";
            if (responseStream != null)
            {
                var streamReader = new StreamReader(responseStream);
                Console.WriteLine("HTTP Response is: ");
                Console.WriteLine(streamReader.ReadToEnd());
                response += streamReader.ReadToEnd();
            }
            if (responseStream != null) { responseStream.Close(); }

            return response;
        }
    }
}
