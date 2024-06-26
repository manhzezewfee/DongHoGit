﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ngay8thang3_Complete.Models.CommonModel
{
    public class SendMail
    {
        public bool SendEmail(string to, string subject, string body, string attachFile)
        {
            try
            {
                MailMessage msg = new MailMessage("", to, subject, body);
                using (var client = new SmtpClient("", 0))
                {
                    client.EnableSsl= true;
                    NetworkCredential credential = new NetworkCredential("", "");
                    client.UseDefaultCredentials= false;
                    client.Credentials = credential;
                    client.Send(msg);
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

    }
}