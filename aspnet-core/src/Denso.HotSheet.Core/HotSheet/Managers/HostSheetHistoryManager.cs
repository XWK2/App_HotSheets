using Abp.Dapper.Repositories;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.HotSheet.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.Logger
{
    public class HostSheetHistoryManager : IHotSheetHistoryManager, ITransientDependency
    {
        private readonly IRepository<HotSheetShipHistory, long> _HotSheetHistoryRepository;
        private readonly IDapperRepository<HotSheetsShip, long> _HotSheetHistoryDapperRepository;

        public HostSheetHistoryManager(
            IRepository<HotSheetShipHistory, long> HotSheetLogRepository,
            IDapperRepository<HotSheetsShip, long> HotSheetHistoryDapperRepository
        )
        {
            _HotSheetHistoryRepository = HotSheetLogRepository;
            _HotSheetHistoryDapperRepository = HotSheetHistoryDapperRepository;
        }

        public void Add(long HotSheetShiptId, HotSheetHistoryType type, long? userIdNotified = null)
        {
            _HotSheetHistoryRepository.Insert(new HotSheetShipHistory
            {
                HotSheetShiptId = HotSheetShiptId,
                HistoryType = type.ToString(),
                UserIdNotified = userIdNotified
            });
        }

        public async Task AddAsync(long HotSheetShiptId, HotSheetHistoryType type, long? userIdNotified = null)
        {
            await _HotSheetHistoryRepository.InsertAsync(new HotSheetShipHistory
            {
                HotSheetShiptId = HotSheetShiptId,
                HistoryType = type.ToString(),
                UserIdNotified = userIdNotified
            });
        }

        public void Add(long HotSheetShiptId, HotSheetHistoryType type, string comments, long? userIdNotified = null)
        {
            _HotSheetHistoryRepository.Insert(new HotSheetShipHistory
            {
                HotSheetShiptId = HotSheetShiptId,
                HistoryType = type.ToString(),
                Comments = comments,
                UserIdNotified = userIdNotified
            });
        }

        public async Task AddAsync(long HotSheetShiptId, HotSheetHistoryType type, string comments, long? userIdNotified = null)
        {
            await _HotSheetHistoryRepository.InsertAsync(new HotSheetShipHistory
            {
                HotSheetShiptId = HotSheetShiptId,
                HistoryType = type.ToString(),
                Comments = comments,
                UserIdNotified = userIdNotified
            });
        }

        public async Task<List<HotSheetHistoryDto>> GetAllAsync(long HotSheetShiptId)
        {
            string sqlQuery = "EXEC GetHistoryByHotSheetShiptId @HotSheetShiptId";

            var sqlParams = new
            {               
                HotSheetShiptId = HotSheetShiptId,             
            };

            var itemsDapper = await _HotSheetHistoryDapperRepository.QueryAsync<HotSheetHistoryDto>(sqlQuery, sqlParams);
            return itemsDapper.ToList();
        }
    }
}
