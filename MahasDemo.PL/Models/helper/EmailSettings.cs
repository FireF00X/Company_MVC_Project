using MahasDemo.PL.Models.AccountViews;
using System.Net;
using System.Net.Mail;

namespace MahasDemo.PL.Models.helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("j.ahmed.elsaied@gmail.com", "nvom iolk ynps tpcs");
            client.Send("j.ahmed.elsaied@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
