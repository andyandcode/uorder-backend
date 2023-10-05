using Models.Tables;

namespace Application.Tables
{
    public interface ITableService
    {
        Task<int> Create(TableCreateRequest req);

        Task<int> Update(TableUpdateRequest req);

        Task<int> Delete(string id);

        List<TableVm> GetAllTable();

        Task<TableVm> GetTableById(string id);
    }
}