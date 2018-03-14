using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AustinStockAdvisor.Library
{
    public static class Utility
    {
        public static void Email(string smtp, int port, string username, string password, string from, string[] recipients, string subject, string body) {
            var client = new SmtpClient(smtp, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };
            for (int a = 0; a < recipients.Length; a++)
            {
                client.Send(from, recipients[a], subject, body);
            }
        }
    }
}
