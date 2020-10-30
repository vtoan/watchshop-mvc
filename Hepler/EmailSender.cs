using System;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace aspcore_watchshop.Hepler {
    public class MailSettings {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class EmailSender : IEmailSender {

        private readonly MailSettings _mailSettings;

        public EmailSender (IOptions<MailSettings> mailSettings) {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync (string email, string subject, string htmlMessage) {
            var message = new MimeMessage ();
            message.Sender = new MailboxAddress (_mailSettings.DisplayName, _mailSettings.Mail);
            message.From.Add (new MailboxAddress (_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add (MailboxAddress.Parse (email));
            message.Subject = subject;
            // Body
            var builder = new BodyBuilder ();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody ();
            // Send Mail
            using var smtp = new MailKit.Net.Smtp.SmtpClient ();
            try {
                smtp.Connect (_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate (_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync (message);
            } catch (Exception ex) {
                Console.Write (ex);
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory ("mailssave");
                var emailsavefile = string.Format (@"mailssave/{0}.eml", Guid.NewGuid ());
                await message.WriteToAsync (emailsavefile);
            }
            smtp.Disconnect (true);
        }
    }

}