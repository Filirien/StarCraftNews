namespace StarCraftNews.Web.Areas.Identity.Services
{
    using Microsoft.Extensions.Options;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        private SendGridOptions options;
        public EmailSender(IOptions<SendGridOptions> options)
        {
            this.options = options.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(this.options.SendGridApiKey);
            var from = new EmailAddress("sambo1993@abv.bg", "Nikolay Georgiev");
            var to = new EmailAddress(email, email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);
            var body = response.Body.ReadAsStringAsync();
            var statusCode = response.StatusCode;
        }
    }
}
