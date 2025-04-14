using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.BackgroundJobs.Args;
using Denso.HotSheet.BackgroundJobs;
using Denso.HotSheet.Interfaces.Dto;
using Denso.HotSheet.HotSheet.AS400;
using Denso.HotSheet.BackgroundJobs.Enums;
using System.Collections.Generic;
using Denso.HotSheet.Sheets.Dto;
using Abp.Dapper.Repositories;
using Denso.HotSheet.HotSheet;
using System.Linq;
using Abp.UI;

namespace Denso.HotSheet.Interfaces
{
    public class InterfaceAppService : HotSheetAppServiceBase, IInterfaceAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IAS400Manager _as400Manager;
        private readonly IDapperRepository<HotSheetsShip, long> _HotSheetDapperRepository;

        public InterfaceAppService(
            UserRegistrationManager userRegistrationManager,
            IBackgroundJobManager backgroundJobManager,
            IAS400Manager as400Manager,
            IDapperRepository<HotSheetsShip, long> HotSheetDapperRepository)
        {
            _userRegistrationManager = userRegistrationManager;
            _backgroundJobManager = backgroundJobManager;
            _as400Manager = as400Manager;
            _HotSheetDapperRepository = HotSheetDapperRepository;
        }

        public async Task CreateUsersFromEmployees(CreateUsersFromEmployeesInput input)
        {
            await _userRegistrationManager.CreateUsersFromEmployeesAsync(input.TenantId, input.EmployeeInternalIds);
        }

        public async Task SendReminders(SendRemindersInput input)
        {
            await _backgroundJobManager.EnqueueAsync<ReminderHotSheetJob, ReminderHotSheetJobArgs>(input);
        }

        public async Task AS400Test()
        {
            await _as400Manager.TestConnection();
        }

        public async Task SendSurveyRequest(long HotSheetShiptId)
        {
            string sqlQuery = "EXEC GetHotSheetById @HotSheetShiptId";
            var sqlParams = new
            {
                HotSheetShiptId = HotSheetShiptId
            };
            var itemsDapper = await _HotSheetDapperRepository.QueryAsync<HotSheetShipItemDto>(sqlQuery, sqlParams);

            var HotSheet = itemsDapper.FirstOrDefault();

            if (HotSheet == null)
            {
                throw new UserFriendlyException($"Hot Sheet Id: {HotSheetShiptId} not found.");
            }
            
            var jobArgs = new SendHotSheetEmailArgs
            {
                HotSheetShiptId = HotSheetShiptId,
                Folio = HotSheet.Folio,
                CreationDate = HotSheet.CreationDate.Value,
                CustomerName = HotSheet.CustomerName,
                DocumentTypeId = HotSheet.DocumentTypeId,
                CreatorFullName = HotSheet.CreatorFullName,
                UsersToNotify = new List<HotSheetEmailItem>(),
            };

            if (HotSheet.IEStaffApproverUserId.HasValue && !string.IsNullOrEmpty(HotSheet.IEStaffEmailAddress))
            {
                jobArgs.NotificationType = HotSheetNotificationType.SurveyRequest;
                jobArgs.UsersToNotify = new List<HotSheetEmailItem> {
                    new HotSheetEmailItem() {
                        UserId = HotSheet.IEStaffApproverUserId,
                        FullName = HotSheet.IEStaffName,
                        EmailAddress = HotSheet.IEStaffEmailAddress,
                    }
                };

                await _backgroundJobManager.EnqueueAsync<SendHotSheetEmailJob, SendHotSheetEmailArgs>(jobArgs);
            }            
        }
    }
}
