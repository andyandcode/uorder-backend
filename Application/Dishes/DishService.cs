using Application.Files;
using Data.EF;
using Data.Entities;
using Hangfire;
using Models.Dishes;
using Utilities.Constants;

namespace Application.Dishes
{
    public class DishService : IDishService
    {
        private readonly UOrderDbContext _context;
        private readonly IFileService _fileService;

        public DishService(UOrderDbContext dbContext, IFileService fileService)
        {
            _context = dbContext;
            _fileService = fileService;
        }

        public async Task<int> Create(DishCreateRequest req)
        {
            var item = new Dish
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                Price = Int32.Parse(string.Concat(req.Price.ToString().Where(char.IsDigit))),
                Type = req.Type,
                CreatedAt = req.CreatedAt,
                Cover = req.Cover != null ? await _fileService.UploadImage(req.Cover) : null,
            };
            _context.Add(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(DishUpdateRequest req)
        {
            var item = new Dish
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                Price = req.Price,
                Type = req.Type,
                CreatedAt = req.CreatedAt,
                Cover = req.Cover != null ? await _fileService.UploadImage(req.Cover) : null,
            };
            _context.Update(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var itemToDelete = await _context.Dishes.FindAsync(id);

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
            var itemToHardDelete = await _context.Dishes.FindAsync(itemId);

            if (itemToHardDelete != null && itemToHardDelete.IsDeleted)
            {
                _context.Dishes.Remove(itemToHardDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UndoDelete(string itemId)
        {
            var itemToUndo = await _context.Dishes.FindAsync(itemId);

            if (itemToUndo != null)
            {
                itemToUndo.IsDeleted = false;
            }

            return await _context.SaveChangesAsync();
        }

        public List<DishVm> GetAll()
        {
            return _context.Dishes.Where(x => x.IsDeleted == false).Select(p => new DishVm()
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Price = p.Price,
                IsActive = p.IsActive,
                Type = p.Type,
                CreatedAt = p.CreatedAt,
                TypeName = p.Type.ToString(),
                CoverLink = p.Cover,
            }).ToList();
        }

        public async Task<DishVm> GetById(string id)
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
                Type = product.Type,
                CreatedAt = product.CreatedAt,
                CoverLink = product.Cover,
            };

            return item;
        }

        public List<DishVm> GetAllById(string id)
        {
            return _context.Dishes.ToList().Where(s => s.Id == id).Select(p => new DishVm()
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Price = p.Price,
                IsActive = p.IsActive,
                Type = p.Type,
                CreatedAt = p.CreatedAt,
                TypeName = p.Type.ToString(),
                CoverLink = p.Cover,
            }).ToList();
        }
    }
}