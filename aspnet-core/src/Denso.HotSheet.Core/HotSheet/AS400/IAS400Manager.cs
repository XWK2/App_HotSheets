using System.Threading.Tasks;

namespace Denso.HotSheet.HotSheet.AS400
{
    public interface IAS400Manager
    {
        Task TestConnection();
        Task<bool> ExportHotSheetToAS400(long HotSheetShiptId);
    }
}
