using Data.EF;
using Data.Entities;
using Models.Dishes;

namespace Application.Dishes
{
    public class DishService : IDishService
    {
        private readonly UOrderDbContext _context;

        public DishService(UOrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> Create(DishCreateRequest req)
        {
            try
            {
                var item = new Dish()
                {
                    Id = req.Id,
                    Name = req.Name,
                    IsActive = req.IsActive,
                    Desc = req.Desc,
                    Price = req.Price,
                    CompletionTime = req.CompletionTime,
                    QtyPerDate = req.QtyPerDate,
                    Type = req.Type,
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

        public async Task<int> Update(DishUpdateRequest req)
        {
            try
            {
                var item = new Dish()
                {
                    Id = req.Id,
                    Name = req.Name,
                    IsActive = req.IsActive,
                    Desc = req.Desc,
                    Price = req.Price,
                    CompletionTime = req.CompletionTime,
                    QtyPerDate = req.QtyPerDate,
                    Type = req.Type,
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
            var product = await _context.Dishes.FindAsync(id);
            if (product == null)
                return 0;

            _context.Dishes.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public List<DishVm> GetAllDish()
        {
            return _context.Dishes.ToList().Select(p => new DishVm()
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Price = p.Price,
                IsActive = p.IsActive,
                CompletionTime = p.CompletionTime,
                QtyPerDate = p.QtyPerDate,
                Type = p.Type,
                CreatedAt = p.CreatedAt,
                TypeName = p.Type.ToString(),
            }).ToList();
        }

        public async Task<DishVm> GetDishById(string id)
        {
            var product = await _context.Dishes.FindAsync(id);
            if (product == null)
                return null;

            var item = new DishVm()
            {
                Id = product.Id,
                Name = product.Name,
                IsActive = product.IsActive,
                Desc = product.Desc,
                Price = product.Price,
                CompletionTime = product.CompletionTime,
                QtyPerDate = product.QtyPerDate,
                Type = product.Type,
                CreatedAt = product.CreatedAt,
            };

            return item;
        }
    }
}