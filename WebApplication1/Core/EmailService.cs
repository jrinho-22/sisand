using Azure;
using Azure.Communication.Email;

namespace WebApplication1.Core
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string plainText = "", string html = "");
    }
    public class EmailService(EmailClient emailClient) : IEmailService
    {
        public async Task SendEmailAsync(string recipient, string subject, string plainText = "", string html = "")
        {
            var emailMessage = new EmailMessage(
                senderAddress: "DoNotReply@631a9bd6-ce40-4420-a8d8-e3408680b180.azurecomm.net",
                recipientAddress: recipient,
                content: new EmailContent(subject)
                {
                    PlainText = plainText,
                    Html = html
                }
            );

            await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
        }
    }
}
