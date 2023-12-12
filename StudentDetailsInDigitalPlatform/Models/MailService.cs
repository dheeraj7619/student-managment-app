using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace StudentDetailsInDigitalPlatform.Models
{
    public class MailService : IMailService
    {
        private readonly MailSetting mailsetting;

        public MailService(IOptions<MailSetting> mailsetting)
        {
            this.mailsetting = mailsetting.Value;
        }
        public async Task  SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailsetting.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody= mailRequest.Body;
            email.Body=builder.ToMessageBody();

            using( var smtp = new SmtpClient())
            {
                smtp.Connect(mailsetting.Host, mailsetting.Port,SecureSocketOptions.StartTls);
                smtp.Authenticate(mailsetting.Mail, mailsetting.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

        }
    }
}
