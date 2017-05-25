﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using System.Web.Configuration;


namespace SendingEmails
{
    class Program
    {
        public static void SendMail(string to, string subject, string body)
        {
            Configuration oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var mailSettings = oConfig.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (mailSettings != null)
            {
                int port = mailSettings.Smtp.Network.Port;
                string from = mailSettings.Smtp.From;
                string host = mailSettings.Smtp.Network.Host;
                string pwd = mailSettings.Smtp.Network.Password;
                string uid = mailSettings.Smtp.Network.UserName;

                var message = new MailMessage
                {
                    From = new MailAddress(@from)
                };
                message.To.Add(new MailAddress(to));
                message.CC.Add(new MailAddress(from));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    Credentials = new NetworkCredential(uid, pwd),
                    EnableSsl = true
                };

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                }
            }
        }
        static void Main(string[] args)
        {
            SendMail("merisahakyan1@gmail.com", "sub", "hello");
        }
    }
}
