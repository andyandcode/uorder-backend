using Data.Enums;
using Models.ActiveLogs;

namespace Application.ActiveLogs
{
    public interface IActiveLogService
    {
        Task CreateActiveLog(ActiveLogCreateRequest req);

        Task<List<ActiveLogVm>> GetActiveLogByEntityId(string id);

        Task<List<ActiveLogVm>> GetActiveLogByEntityType(EntityType type);
    }
}