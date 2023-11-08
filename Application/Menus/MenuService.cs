using Application.Dishes;
using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dishes;
using Models.Menus;
using Utilities.Common;

namespace Application.Menus
{
    public class MenuService : IMenuService
    {
        private readonly UOrderDbContext _context;
        private readonly IDishService _dishService;
        private readonly IdGeneration gen = new IdGeneration();

        public MenuService(UOrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> Create(MenuCreateRequest req)
        {
            var id = req.Id;

            var item = new Menu()
            {
                Id = id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };
            foreach (var child in req.Dishes)
            {
                DishMenu connect = new DishMenu() { DishId = child, MenuId = id };
                _context.DishMenus.Add(connect);
            }

            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(MenuUpdateRequest req)
        {
            var id = req.Id;

            var item = new Menu()
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };
            var assets = await _context.DishMenus.Where(a => a.MenuId.Contains(a.MenuId)).ToArrayAsync();

            if (assets == null)
            {
                return 0;
            }
            else
            {
                _context.DishMenus.RemoveRange(assets);
            }

            foreach (var child in req.Dishes)
            {
                DishMenu connect = new DishMenu() { DishId = child, MenuId = id };
                _context.DishMenus.Add(connect);
            }

            _context.Update(item);
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

        public async Task<List<MenuVm>> GetAll()
        {
            var result = await _context.Menus.Include(one => one.Dishes).ThenInclude(dishes => dishes.Dish).Select(p => new MenuVm()
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                Dishes = p.Dishes.ToList().Select(i => new DishVm()
                {
                    Key = i.Dish.Id,
                    Id = i.Dish.Id,
                    Name = i.Dish.Name,
                    Desc = i.Dish.Desc,
                    Price = i.Dish.Price,
                    IsActive = i.Dish.IsActive,
                    CompletionTime = i.Dish.CompletionTime,
                    QtyPerDay = i.Dish.QtyPerDay,
                    Type = i.Dish.Type,
                    CreatedAt = i.Dish.CreatedAt,
                    TypeName = i.Dish.Type.ToString(),
                }).ToList(),
            }).ToListAsync();
            return result;
        }

        public async Task<MenuVm> GetById(string id)
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