using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Denso.HotSheet.BackgroundJobs.Args;
using Denso.HotSheet.BackgroundJobs.Enums;
using Denso.HotSheet.Configuration;
using Denso.HotSheet.Email;
using Denso.HotSheet.HotSheet.Logger;
using Denso.HotSheet.HotSheet.Enums;
using Denso.HotSheet.HotSheet.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Denso.HotSheet.BackgroundJobs
{
    public class ReminderHotSheetJob : BackgroundJob<ReminderHotSheetJobArgs>, ITransientDependency
    {
        private readonly IHostEnvironment _env;
        private readonly IEmailManager _emailManager;
        private readonly IConfigurationRoot _appConfiguration;
        IRepository<EmailTemplate> _emailTemplateRepository;
        private readonly IHotSheetHistoryManager _shippingHistoryManager;

        public ReminderHotSheetJob(IHostEnvironment env, IEmailManager emailManager, IAppConfigurationAccessor appConfigurationAccessor,
            IRepository<EmailTemplate> emailTemplateRepository, IHotSheetHistoryManager shippingHistoryManager)
        {
            _env = env;
            _emailManager = emailManager;
            _emailTemplateRepository = emailTemplateRepository;
            _appConfiguration = appConfigurationAccessor.Configuration;
            _shippingHistoryManager = shippingHistoryManager;
        }

        [UnitOfWork]
        public override void Execute(ReminderHotSheetJobArgs args)
        {
            bool emailSent = false;

            string clientURL = _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/');
            string webAppLink = clientURL + "app/shipping/instructions/pending-for-approval";
            string webAppLinkText = "Pendientes por Aprobar - Hot Sheet";

            if (args.NotificationType == HotSheetNotificationType.ApprovalRequestReminder)
            {
                var approvalRequestReminderTemplate = _emailTemplateRepository.GetAll()
                    .Where(c => c.Name == "ApprovalRequestReminder")
                    .FirstOrDefault();

                if (approvalRequestReminderTemplate != null)
                {
                    string htmlRows = "";
                    foreach (var shippingItem in args.HotSheets)
                    {
                        htmlRows += "<tr>" +
                            "<td>" + shippingItem.Folio  + "</td>" +
                            "<td>" + shippingItem.CreationDate.ToString("MM/dd/yyyy HH:mm") + "</td>" +
                            "<td>" + shippingItem.CreatorFullName + "</td>" +
                            "<td>" + getDocType(shippingItem.DocumentTypeId) + "</td>" +
                            "<td>" + shippingItem.CustomerName + "</td>" +
                            "</tr>";
                    }

                    string body = approvalRequestReminderTemplate.Body
                                .Replace("{fullName}", args.FullName)
                                .Replace("{rows}", htmlRows);

                    _emailManager.Send(args.EmailAddress, approvalRequestReminderTemplate.Subject, body, webAppLink, webAppLinkText);
                    emailSent = true;
                }
            }

            if (emailSent)
            {
                foreach (var hotSheetItem in args.HotSheets)
                {
                    _shippingHistoryManager.Add(hotSheetItem.HotSheetShiptId, HotSheetHistoryType.EmailNotificationSent, hotSheetItem.CreatorUserId);
                }
            }
        }

        private string getDocType(int documentTypeId)
        {
            string docType = "";

            switch (documentTypeId)
            {
                case 1:
                    docType = "Air";
                    break;
                case 2:
                    docType = "Ground";
                    break;
                case 3:
                    docType = "Sea";
                    break;
                default:
                    // code block
                    break;
            }

            return docType;
        }
    }
}
