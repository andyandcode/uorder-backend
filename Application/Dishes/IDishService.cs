using Models.Dishes;

namespace Application.Dishes
{
    public interface IDishService
    {
        Task<int> Create(DishCreateRequest req);

        Task<int> Update(DishUpdateRequest req);

        Task<int> Delete(string id);

        Task HardDelete(string itemId);

        Task<int> UndoDelete(string itemId);

        List<DishVm> GetAll();

        Task<DishVm> GetById(string id);

        List<DishVm> GetAllById(string id);
    }
}