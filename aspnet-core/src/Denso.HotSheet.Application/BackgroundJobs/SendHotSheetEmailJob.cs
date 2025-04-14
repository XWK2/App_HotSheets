using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Security;
using Denso.HotSheet.BackgroundJobs.Args;
using Denso.HotSheet.BackgroundJobs.Enums;
using Denso.HotSheet.Configuration;
using Denso.HotSheet.Email;
using Denso.HotSheet.HotSheet.Enums;
using Denso.HotSheet.HotSheet.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Web;

namespace Denso.HotSheet.BackgroundJobs
{
    public class SendHotSheetEmailJob : BackgroundJob<SendHotSheetEmailArgs>, ITransientDependency
    {
        private readonly IHostEnvironment _env;
        private readonly IEmailManager _emailManager;
        private readonly IConfigurationRoot _appConfiguration;
        IRepository<EmailTemplate> _emailTemplateRepository;
        private readonly IHotSheetHistoryManager _shippingHistoryManager;

        public SendHotSheetEmailJob(IHostEnvironment env, IEmailManager emailManager, IAppConfigurationAccessor appConfigurationAccessor,
            IRepository<EmailTemplate> emailTemplateRepository, IHotSheetHistoryManager shippingHistoryManager)
        {
            _env = env;
            _emailManager = emailManager;
            _emailTemplateRepository = emailTemplateRepository;
            _appConfiguration = appConfigurationAccessor.Configuration;
            _shippingHistoryManager = shippingHistoryManager;
        }

        [UnitOfWork]
        public override void Execute(SendHotSheetEmailArgs args)
        {
            if(args.HotSheetShiptId > 0)
            {
                string clientURL = _appConfiguration["App:ClientRootAddress"].EnsureEndsWith('/');
                //string webAppLink = clientURL + "app/shipping/instructions-details/check-approval/" + HttpUtility.UrlEncode(args.IdEncrypted);

                string webAppLink = clientURL + "app/shipping/instructions-details/check-approval/" + args.HotSheetShiptId;
                // if (_env.IsDevelopment())
                // {
                //    webAppLink = clientURL + "app/shipping/instructions-details/check-approval/" + args.IdEncrypted;
                // }
                string webAppLinkText = "Hot Sheet - Verificar Autorización";
                bool emailSent = false;

                if (args.NotificationType == HotSheetNotificationType.ApprovalRequestToManager)
                {
                    var approvalRequestToManagerTemplate = _emailTemplateRepository.GetAll()
                        .Where(c => c.Name == "ApprovalRequestToManager")
                        .FirstOrDefault();

                    if (approvalRequestToManagerTemplate != null)
                    {
                        emailSent = false;
                        foreach (var userInfo in args.UsersToNotify)
                        {
                            string body = approvalRequestToManagerTemplate.Body
                                .Replace("{folio}", args.Folio)
                                .Replace("{fullName}", userInfo.FullName)
                                .Replace("{creationDate}", args.CreationDate.ToString("MM/dd/yyyy HH:mm"))
                                .Replace("{author}", args.CreatorFullName)
                                .Replace("{type}", getDocType(args.DocumentTypeId))
                                .Replace("{customerName}", args.CustomerName);

                            webAppLink += "/1";

                            string subject = approvalRequestToManagerTemplate.Subject.Replace("{folio}", args.Folio);

                            _emailManager.Send(userInfo.EmailAddress, subject, body, webAppLink, webAppLinkText);
                            emailSent = true;
                        }
                    }
                }

                if (args.NotificationType == HotSheetNotificationType.ApprovalRequestToImpoExpoStaff)
                {
                    var approvalRequestToImpoExpoStaffTemplate = _emailTemplateRepository.GetAll()
                        .Where(c => c.Name == "ApprovalRequestToImpoExpoStaff")
                        .FirstOrDefault();

                    if (approvalRequestToImpoExpoStaffTemplate != null)
                    {
                        emailSent = false;
                        foreach (var userInfo in args.UsersToNotify)
                        {
                            string body = approvalRequestToImpoExpoStaffTemplate.Body
                                .Replace("{folio}", args.Folio)
                                .Replace("{fullName}", userInfo.FullName)
                                .Replace("{creationDate}", args.CreationDate.ToString("MM/dd/yyyy HH:mm"))
                                .Replace("{author}", args.CreatorFullName)
                                .Replace("{type}", getDocType(args.DocumentTypeId))
                                .Replace("{customerName}", args.CustomerName);

                            webAppLink += "/2";

                            string subject = approvalRequestToImpoExpoStaffTemplate.Subject.Replace("{folio}", args.Folio);

                            _emailManager.Send(userInfo.EmailAddress, subject, body, webAppLink, webAppLinkText);
                            emailSent = true;
                        }
                    }
                }

                if (args.NotificationType == HotSheetNotificationType.ApprovalRequestToAccountingStaff)
                {
                    var approvalRequestToAccountingStaffTemplate = _emailTemplateRepository.GetAll()
                        .Where(c => c.Name == "ApprovalRequestToAccountingStaff")
                        .FirstOrDefault();

                    if (approvalRequestToAccountingStaffTemplate != null)
                    {
                        emailSent = false;
                        foreach (var userInfo in args.UsersToNotify)
                        {
                            string body = approvalRequestToAccountingStaffTemplate.Body
                                .Replace("{folio}", args.Folio)
                                .Replace("{fullName}", userInfo.FullName)
                                .Replace("{creationDate}", args.CreationDate.ToString("MM/dd/yyyy HH:mm"))
                                .Replace("{author}", args.CreatorFullName)
                                .Replace("{type}", getDocType(args.DocumentTypeId))
                                .Replace("{customerName}", args.CustomerName);

                            webAppLink += "/3";

                            string subject = approvalRequestToAccountingStaffTemplate.Subject.Replace("{folio}", args.Folio);

                            _emailManager.Send(userInfo.EmailAddress, subject, body, webAppLink, webAppLinkText);
                            emailSent = true;
                        }
                    }
                }

                if (args.NotificationType == HotSheetNotificationType.SurveyRequest)
                {
                    webAppLink = clientURL + "app/shipping/instructions-details/survey/" + args.HotSheetShiptId;
                    webAppLinkText = "Hot Sheet - Contestar Encuesta";

                    var surveyRequestTemplate = _emailTemplateRepository.GetAll()
                        .Where(c => c.Name == "SurveyRequest")
                        .FirstOrDefault();

                    if (surveyRequestTemplate != null)
                    {
                        emailSent = false;
                        foreach (var userInfo in args.UsersToNotify)
                        {
                            string body = surveyRequestTemplate.Body
                                .Replace("{folio}", args.Folio)
                                .Replace("{fullName}", userInfo.FullName)
                                .Replace("{creationDate}", args.CreationDate.ToString("MM/dd/yyyy HH:mm"))
                                .Replace("{author}", args.CreatorFullName)
                                .Replace("{type}", getDocType(args.DocumentTypeId))
                                .Replace("{customerName}", args.CustomerName);

                            string subject = surveyRequestTemplate.Subject.Replace("{folio}", args.Folio);

                            _emailManager.Send(userInfo.EmailAddress, subject, body, webAppLink, webAppLinkText);
                            emailSent = true;
                        }
                    }
                }

                if (args.NotificationType == HotSheetNotificationType.ApprovalRequestRejected)
                {
                    webAppLink = clientURL + "app/shipping/instructions";
                    webAppLinkText = "Hot Sheet";

                    var approvalRequestRejectedTemplate = _emailTemplateRepository.GetAll()
                        .Where(c => c.Name == "ApprovalRequestRejected")
                        .FirstOrDefault();

                    if (approvalRequestRejectedTemplate != null)
                    {
                        emailSent = false;
                        foreach (var userInfo in args.UsersToNotify)
                        {
                            string body = approvalRequestRejectedTemplate.Body
                                .Replace("{folio}", args.Folio)
                                .Replace("{fullName}", userInfo.FullName)
                                .Replace("{creationDate}", args.CreationDate.ToString("MM/dd/yyyy HH:mm"))
                                .Replace("{author}", args.CreatorFullName)
                                .Replace("{type}", getDocType(args.DocumentTypeId))
                                .Replace("{customerName}", args.CustomerName)
                                .Replace("{rejectedBy}", args.RejectedBy)
                                .Replace("{reasonRejection}", args.ReasonRejection);

                            string subject = approvalRequestRejectedTemplate.Subject.Replace("{folio}", args.Folio);

                            _emailManager.Send(userInfo.EmailAddress, subject, body, webAppLink, webAppLinkText);
                            emailSent = true;
                        }
                    }
                }

                if (args.NotificationType == HotSheetNotificationType.ApprovalRequestApproved)
                {
                    webAppLink = clientURL + "app/shipping/instructions";
                    webAppLinkText = "Hot Sheet";

                    var approvalRequestApprovedTemplate = _emailTemplateRepository.GetAll()
                        .Where(c => c.Name == "ApprovalRequestApproved")
                        .FirstOrDefault();

                    if (approvalRequestApprovedTemplate != null)
                    {
                        emailSent = false;
                        foreach (var userInfo in args.UsersToNotify)
                        {
                            string body = approvalRequestApprovedTemplate.Body
                                .Replace("{folio}", args.Folio)
                                .Replace("{fullName}", userInfo.FullName)
                                .Replace("{creationDate}", args.CreationDate.ToString("MM/dd/yyyy HH:mm"))
                                .Replace("{author}", args.CreatorFullName)
                                .Replace("{type}", getDocType(args.DocumentTypeId))
                                .Replace("{customerName}", args.CustomerName);

                            string subject = approvalRequestApprovedTemplate.Subject.Replace("{folio}", args.Folio);

                            _emailManager.Send(userInfo.EmailAddress, subject, body, webAppLink, webAppLinkText);
                            emailSent = true;
                        }
                    }
                }

                if (emailSent)
                {
                    _shippingHistoryManager.Add(args.HotSheetShiptId, HotSheetHistoryType.EmailNotificationSent, args.UsersToNotify.First().UserId);
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
