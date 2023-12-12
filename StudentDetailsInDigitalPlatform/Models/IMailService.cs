namespace StudentDetailsInDigitalPlatform.Models
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
