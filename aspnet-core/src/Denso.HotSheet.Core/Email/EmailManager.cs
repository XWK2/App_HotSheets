using Abp.Dependency;
using Abp.Extensions;
using Abp.Net.Mail;
using Denso.HotSheet.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Mail;

namespace Denso.HotSheet.Email
{
    public class EmailManager : IEmailManager, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly IHostEnvironment _env;
        private readonly IEmailManager _emailManager;
        private readonly IConfigurationRoot _appConfiguration;

        public EmailManager(IEmailSender emailSender, IHostEnvironment env, IAppConfigurationAccessor appConfigurationAccessor)
        {
            _emailSender = emailSender;
            _env = env;

            _appConfiguration = appConfigurationAccessor.Configuration;
        }

        public void Send(string emailAddress, string subject, string body, string webAppLink = null, string webAppLinkText = null)
        {
            try
            {
                string clientURL = _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/');

                var mailMessage = new MailMessage();
                mailMessage.To.Add(emailAddress);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.AlternateViews.Add(EmailHelper.CreateHtmlMessageWithImages(body, _env.ContentRootPath, clientURL, webAppLink, webAppLinkText));

                _emailSender.Send(mailMessage);
            }
            catch (Exception exc)
            {

            }
        }
    }
}
