using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Platform.ReferencialData.WebAPI.Controllers
{
    public class EmailHelperSMTP
    {
        public bool SendEmail(string userEmail, string emailBody, IConfiguration configuration)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("mail.foodme@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Mail Confirmation";
            email.Body = new TextPart(TextFormat.Html) { Text = emailBody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.Authenticate("mail.foodme@gmail.com", "esmwibnsylcfvelh");
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
    }
}
