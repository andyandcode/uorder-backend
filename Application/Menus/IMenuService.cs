using Models.Menus;

namespace Application.Menus
{
    public interface IMenuService
    {
        Task<int> Create(MenuCreateRequest req);

        Task<int> Update(MenuUpdateRequest req);

        Task<int> Delete(string id);

        List<MenuVm> GetAllMenu();

        Task<MenuVm> GetMenuById(string id);
    }
}