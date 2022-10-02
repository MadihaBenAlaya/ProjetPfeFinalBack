using AppRecrutement.Configuration;
using AppRecrutement.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace AppRecrutement.Services
{
    public interface IEmailService
    {
        bool SendEmail(EmailData emailData);

        public class EmailService : IEmailService
        {
            EmailSettings _emailSettings = null;
            public EmailService(IOptions<EmailSettings> options)
            {
                _emailSettings = options.Value;
            }

            public bool SendEmail(EmailData emailData)
            {
                try
                {


                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(_emailSettings.EmailId, _emailSettings.Password),
                        EnableSsl = true,
                    };

                    smtpClient.Send("addinn.tunisie@gmail.com", emailData.EmailToId, emailData.EmailSubject, emailData.EmailBody);

                    return true;
                }
                catch (Exception ex)
                {
                    //Log Exception Details
                    return false;
                }
            }
        }
    }
}
