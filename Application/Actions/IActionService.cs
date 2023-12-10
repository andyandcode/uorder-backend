using Models.Tables;

namespace Application.Actions
{
    public interface IActionService
    {
        Task<TableVm> CallStaff(string tableId);
    }
}