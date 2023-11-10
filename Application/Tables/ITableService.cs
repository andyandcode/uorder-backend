using Models.Tables;

namespace Application.Tables
{
    public interface ITableService
    {
        Task<int> Create(TableCreateRequest req);

        Task<int> Update(TableUpdateRequest req);

        Task<int> Delete(string id);

        Task<List<TableVm>> GetAll();

        Task<TableVm> GetById(string id);
    }
}