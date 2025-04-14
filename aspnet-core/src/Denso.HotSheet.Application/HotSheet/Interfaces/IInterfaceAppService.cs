using System.Threading.Tasks;
using Abp.Application.Services;
using Denso.HotSheet.Interfaces.Dto;

namespace Denso.HotSheet.Interfaces
{
    public interface IInterfaceAppService : IApplicationService
    {
        Task CreateUsersFromEmployees(CreateUsersFromEmployeesInput input);
        Task SendReminders(SendRemindersInput input);
        Task SendSurveyRequest(long HotSheetShiptId);
    }
}
