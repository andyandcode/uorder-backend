using Models.Dishes;

namespace Application.Dishes
{
    public interface IDishService
    {
        Task<int> Create(DishCreateRequest req);

        Task<int> Update(DishUpdateRequest req);

        Task<int> Delete(string id);

        List<DishVm> GetAll();

        Task<DishVm> GetById(string id);
    }
}