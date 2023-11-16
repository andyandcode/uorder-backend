using Application.SystemSettings;
using Data.EF;
using Data.Entities;
using Models.Tables;

namespace Application.Tables
{
    public class TableService : ITableService
    {
        private readonly UOrderDbContext _context;
        private readonly ISystemSettingService _systemSettingService;

        public TableService(UOrderDbContext dbContext, ISystemSettingService systemSettingService)
        {
            _context = dbContext;
            _systemSettingService = systemSettingService;
        }

        public async Task<int> Create(TableCreateRequest req)
        {
            var setting = await _systemSettingService.GetSettings();
            var item = new Table()
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
                Route = setting.Domain + "/booking/" + req.Id,
            };
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(TableUpdateRequest req)
        {
            var setting = await _systemSettingService.GetSettings();
            var item = new Table()
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
                Route = setting.Domain + "/booking/" + req.Id,
            };
            _context.Update(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Tables.FindAsync(id);
            if (item == null)
                return 0;

            _context.Tables.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<TableVm>> GetAll()
        {
            var setting = await _systemSettingService.GetSettings();
            return _context.Tables.ToList().Select(p => new TableVm()
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                Route = setting.Domain + "/booking/" + p.Id,
            }).ToList();
        }

        public async Task<TableVm> GetById(string id)
        {
            var target = await _context.Tables.FindAsync(id);
            if (target == null)
                return null;

            var item = new TableVm()
            {
                Key = target.Id,
                Id = target.Id,
                Name = target.Name,
                IsActive = target.IsActive,
                Desc = target.Desc,
                CreatedAt = target.CreatedAt,
                Route = target.Route,
            };

            return item;
        }
    }
}