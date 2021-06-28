using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Utils
{
    public class Mailing
    {
        internal static void SendMail(string email, string guid, string name)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("");
                mail.To.Add(email);
                mail.Subject = $"RESET PASSWORD {DateTime.Now}";
                mail.Body = $"Hai {name},\nIni adalah password baru kamu : {guid}";
                using (SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587))
                {
                    smpt.Credentials = new System.Net.NetworkCredential("", "");
                    smpt.EnableSsl = true;
                    smpt.Send(mail);
                }
            }
        }
    }
}
