using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.HotSheet.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.Logger
{
    public interface IHotSheetHistoryManager
    {
        void Add(long HotSheetShiptId, HotSheetHistoryType type, long? userIdNotified = null);
        void Add(long HotSheetShiptId, HotSheetHistoryType type, string comments, long? userIdNotified = null);

        Task AddAsync(long HotSheetShiptId, HotSheetHistoryType type, long? userIdNotified = null);
        Task AddAsync(long HotSheetShiptId, HotSheetHistoryType type, string comments, long? userIdNotified = null);

        Task<List<HotSheetHistoryDto>> GetAllAsync(long HotSheetShiptId);
    }
}
