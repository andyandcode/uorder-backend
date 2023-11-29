using Application.ActiveLogs;
using Application.Files;
using Data.EF;
using Data.Entities;
using Data.Enums;
using Models.ActiveLogs;
using Models.Dishes;

namespace Application.Dishes
{
    public class DishService : IDishService
    {
        private readonly UOrderDbContext _context;
        private readonly IActiveLogService _activeLogService;
        private readonly IFileService _fileService;

        public DishService(UOrderDbContext dbContext, IActiveLogService activeLogService, IFileService fileService)
        {
            _context = dbContext;
            _activeLogService = activeLogService;
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
                CompletionTime = req.CompletionTime,
                QtyPerDay = req.QtyPerDay,
                Type = req.Type,
                CreatedAt = req.CreatedAt,
                Cover = req.Cover != null ? await _fileService.UploadImage(req.Cover) : null,
            };
            _context.Add(item);

            var log = new ActiveLogCreateRequest
            {
                EntityId = req.Id,
                Timestamp = req.CreatedAt,
                EntityType = EntityType.Dish,
                ActiveLogActionType = ActiveLogActionType.Create,
            };
            await _activeLogService.CreateActiveLog(log);

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
                CompletionTime = req.CompletionTime,
                QtyPerDay = req.QtyPerDay,
                Type = req.Type,
                CreatedAt = req.CreatedAt,
                Cover = req.Cover != null ? await _fileService.UploadImage(req.Cover) : null,
            };
            _context.Update(item);

            var log = new ActiveLogCreateRequest
            {
                EntityId = req.Id,
                Timestamp = DateTime.Now,
                EntityType = EntityType.Dish,
                ActiveLogActionType = ActiveLogActionType.Update,
            };
            await _activeLogService.CreateActiveLog(log);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var product = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(product);

            var log = new ActiveLogCreateRequest
            {
                EntityId = id,
                Timestamp = DateTime.Now,
                EntityType = EntityType.Dish,
                ActiveLogActionType = ActiveLogActionType.Delete,
            };
            await _activeLogService.CreateActiveLog(log);

            return await _context.SaveChangesAsync();
        }

        public List<DishVm> GetAll()
        {
            return _context.Dishes.ToList().Select(p => new DishVm()
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                Price = p.Price,
                IsActive = p.IsActive,
                CompletionTime = p.CompletionTime,
                QtyPerDay = p.QtyPerDay,
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
                CompletionTime = product.CompletionTime,
                QtyPerDay = product.QtyPerDay,
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
                CompletionTime = p.CompletionTime,
                QtyPerDay = p.QtyPerDay,
                Type = p.Type,
                CreatedAt = p.CreatedAt,
                TypeName = p.Type.ToString(),
                CoverLink = p.Cover,
            }).ToList();
        }
    }
}