using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Models.Tables;

namespace Application.Tables
{
    public interface ITableService
    {
        Task<int> Create(TableCreateRequest req);

        Task<int> Update(TableUpdateRequest req);

        Task<int> Delete(string id);

        Task HardDelete(string itemId);

        Task<int> UndoDelete(string itemId);

        Task<List<TableVm>> GetAll();

        Task<TableVm> GetById(string id);

        Task<int> UpdateStatus(string id, JsonPatchDocument<Table> patchDoc);
    }
}