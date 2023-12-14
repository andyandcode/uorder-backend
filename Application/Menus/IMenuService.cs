using Models.Menus;

namespace Application.Menus
{
    public interface IMenuService
    {
        Task<int> Create(MenuCreateRequest req);

        Task<int> Update(MenuUpdateRequest req);

        Task<int> Delete(string id);

        Task HardDelete(string itemId);

        Task<int> UndoDelete(string itemId);

        Task<int> RemoveDishFromMenu(RemoveDishFromMenuRequest req);

        Task<List<MenuVm>> GetAll();

        Task<List<MenuVm>> GetAllAvailable();

        Task<MenuVm> GetById(string id);
    }
}