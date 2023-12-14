using Data.EF;
using Data.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Models.Dishes;
using Models.Menus;
using Utilities.Constants;

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
            var id = req.Id;
            var item = new Menu
            {
                Id = id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };

            _context.Add(item);

            if (req.Dishes != null)
            {
                foreach (var child in req.Dishes)
                {
                    var connect = new DishMenu { DishId = child, MenuId = id };
                    _context.DishMenus.Add(connect);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(MenuUpdateRequest req)
        {
            var item = new Menu
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };

            var assets = _context.DishMenus.Where(a => a.MenuId == req.Id).ToList();
            if (assets != null)
            {
                _context.DishMenus.RemoveRange(assets);
            }

            foreach (var child in req.Dishes)
            {
                var connect = new DishMenu { DishId = child, MenuId = req.Id };
                _context.DishMenus.Add(connect);
            }
            _context.Update(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var itemToDelete = await _context.Menus.FindAsync(id);

            if (itemToDelete != null)
            {
                itemToDelete.IsDeleted = true;
                BackgroundJob.Schedule(() => HardDelete(itemToDelete.Id), TimeSpan.FromSeconds(SystemConstants.ScheduledDeletionTime));
            }

            await _context.SaveChangesAsync();
            return SystemConstants.ScheduledDeletionTime;
        }

        public async Task HardDelete(string itemId)
        {
            var itemToHardDelete = await _context.Menus.FindAsync(itemId);

            if (itemToHardDelete != null && itemToHardDelete.IsDeleted)
            {
                _context.Menus.Remove(itemToHardDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UndoDelete(string itemId)
        {
            var itemToUndo = await _context.Menus.FindAsync(itemId);

            if (itemToUndo != null)
            {
                itemToUndo.IsDeleted = false;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveDishFromMenu(RemoveDishFromMenuRequest req)
        {
            var item = _context.DishMenus.FirstOrDefault(x => x.DishId == req.DishId && x.MenuId == req.MenuId);
            if (item == null)
                return 0;

            _context.DishMenus.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<MenuVm>> GetAll()
        {
            var result = await _context.Menus.Where(x => x.IsDeleted == false).Include(one => one.Dishes).ThenInclude(dishes => dishes.Dish).Select(p => new MenuVm
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                Dishes = p.Dishes.ToList().Select(i => new DishVm
                {
                    Key = i.Dish.Id,
                    Id = i.Dish.Id,
                    Name = i.Dish.Name,
                    Desc = i.Dish.Desc,
                    Price = i.Dish.Price,
                    IsActive = i.Dish.IsActive,
                    Type = i.Dish.Type,
                    CreatedAt = i.Dish.CreatedAt,
                    TypeName = i.Dish.Type.ToString(),
                    CoverLink = i.Dish.Cover,
                }).ToList(),
            }).ToListAsync();
            return result;
        }

        public async Task<List<MenuVm>> GetAllAvailable()
        {
            var result = await _context.Menus.Include(one => one.Dishes).ThenInclude(dishes => dishes.Dish).Where(menu => menu.IsActive == true).Select(p => new MenuVm
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                Dishes = p.Dishes.ToList().Where(dish => dish.Dish.IsActive == true).Select(i => new DishVm
                {
                    Key = i.Dish.Id,
                    Id = i.Dish.Id,
                    Name = i.Dish.Name,
                    Desc = i.Dish.Desc,
                    Price = i.Dish.Price,
                    IsActive = i.Dish.IsActive,
                    Type = i.Dish.Type,
                    CreatedAt = i.Dish.CreatedAt,
                    TypeName = i.Dish.Type.ToString(),
                    CoverLink = i.Dish.Cover,
                }).ToList(),
            }).ToListAsync();
            return result;
        }

        public async Task<MenuVm> GetById(string id)
        {
            var target = await _context.Menus.FindAsync(id);
            if (target == null)
                return null;

            var item = new MenuVm
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