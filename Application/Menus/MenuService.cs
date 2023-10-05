using Data.EF;
using Data.Entities;
using Models.Menus;

namespace Application.Menus
{
    public class MenuService : IMenuService
    {
        private readonly UOrderDbContext _context;

        public MenuService(UOrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> Create(MenuCreateRequest req)
        {
            try
            {
                var item = new Menu()
                {
                    Id = req.Id,
                    Name = req.Name,
                    IsActive = req.IsActive,
                    Desc = req.Desc,
                    CreatedAt = req.CreatedAt,
                };
                _context.Add(item);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(MenuUpdateRequest req)
        {
            try
            {
                var item = new Menu()
                {
                    Id = req.Id,
                    Name = req.Name,
                    IsActive = req.IsActive,
                    Desc = req.Desc,
                    CreatedAt = req.CreatedAt,
                };
                _context.Update(item);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Menus.FindAsync(id);
            if (item == null)
                return 0;

            _context.Menus.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public List<MenuVm> GetAllMenu()
        {
            return _context.Menus.ToList().Select(p => new MenuVm()
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
            }).ToList();
        }

        public async Task<MenuVm> GetMenuById(string id)
        {
            var target = await _context.Menus.FindAsync(id);
            if (target == null)
                return null;

            var item = new MenuVm()
            {
                Id = target.Id,
                Name = target.Name,
                IsActive = target.IsActive,
                Desc = target.Desc,
                CreatedAt = target.CreatedAt,
            };

            return item;
        }
    }
}