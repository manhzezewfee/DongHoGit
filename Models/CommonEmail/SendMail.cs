using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Configuration;

namespace ngay8thang3_Complete.Models.CommonEmail
{
    public class SendMail
    {
        
        public bool SendEmail(string to, string subject, string body, string attachFile)
        {
            //SmtpClient smtp = new SmtpClient();
            //try
            //{
            //    //ĐỊA CHỈ SMTP Server
            //    smtp.Host = "smtp.gmail.com";
            //    //Cổng SMTP
            //    smtp.Port = 587;
            //    //SMTP yêu cầu mã hóa dữ liệu theo SSL
            //    smtp.EnableSsl = true;
            //    //UserName và Password của mail
            //    smtp.Credentials = new NetworkCredential("manhzeze154@gmail.com", "893172pro");

            //    //Tham số lần lượt là địa chỉ người gửi, người nhận, tiêu đề và nội dung thư
            //    smtp.Send("manhzeze154@gmail.com", txtTo.Text, txtSubject.Text, txtMessage.Text);
            //    lbStatus.Text = "Sent.";
            //}
            //catch (Exception ex)
            //{
            //    lbStatus.Text = ex.Message;
            //}


            try
            {
                MailMessage msg = new MailMessage(constantHelper.emailSender, to, subject, body);
                using (var client = new SmtpClient(constantHelper.emailSender, 0))
                {
                    client.EnableSsl = true;
                    NetworkCredential credential = new NetworkCredential(constantHelper.emailSender, constantHelper.emailSender);
                    client.UseDefaultCredentials = false;
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