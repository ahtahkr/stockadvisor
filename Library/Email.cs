using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Library
{
    public static class Email
    {
        public static void Send(string smtp, int port, string username, string password, string from, string[] recipients, string subject, string body)
        {
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
