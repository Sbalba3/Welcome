using Demo.DAl.Models;

using System.Net;
using System.Net.Mail;

namespace Welcome.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("Salahbalbaa.sb@gmail.com", "oqyjfwgiohxrdijm");
            client.Send("Salahbalbaa.sb@gmail.com", email.To, email.Subject, email.Body);

        } 
    }
}
