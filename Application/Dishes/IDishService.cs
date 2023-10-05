using Models.Dishes;

namespace Application.Dishes
{
    public interface IDishService
    {
        Task<int> Create(DishCreateRequest req);

        Task<int> Update(DishUpdateRequest req);

        Task<int> Delete(string id);

        List<DishVm> GetAllDish();

        Task<DishVm> GetDishById(string id);
    }
}