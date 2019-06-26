using IoTMon.Models.Notifiers;
using IoTMon.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IoTMon.Services.Notifiers
{
    public class EmailNotifier : IAlertNotifier
    {
        private readonly EmailNotifyConfig config;
        public EmailNotifier(EmailNotifyConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<HttpStatusCode> Execute(object properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            EmailAlert props = (EmailAlert)properties;

            var apiKey = Environment.GetEnvironmentVariable(this.config.EnvApiKey, EnvironmentVariableTarget.User);
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(this.config.SenderEmail, this.config.SenderName);
            var to = new EmailAddress(props.ReceiverEmail, props.ReceiverName);

            var msg = MailHelper.CreateSingleEmail(from, to, props.Subject, props.TextContent, props.HtmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            return response.StatusCode;
        }
    }
}
