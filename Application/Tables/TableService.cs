using Application.ActiveLogs;
using Application.SystemSettings;
using Data.EF;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Models.ActiveLogs;
using Models.Tables;

namespace Application.Tables
{
    public class TableService : ITableService
    {
        private readonly UOrderDbContext _context;
        private readonly ISystemSettingService _systemSettingService;
        private readonly IActiveLogService _activeLogService;

        public TableService(UOrderDbContext dbContext, ISystemSettingService systemSettingService, IActiveLogService activeLogService)
        {
            _context = dbContext;
            _systemSettingService = systemSettingService;
            _activeLogService = activeLogService;
        }

        public async Task<int> Create(TableCreateRequest req)
        {
            var item = new Table()
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };
            _context.Add(item);

            var log = new ActiveLogCreateRequest
            {
                EntityId = req.Id,
                Timestamp = req.CreatedAt,
                EntityType = EntityType.Table,
                ActiveLogActionType = ActiveLogActionType.Create,
            };
            await _activeLogService.CreateActiveLog(log);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(TableUpdateRequest req)
        {
            var item = new Table()
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };
            _context.Update(item);

            var log = new ActiveLogCreateRequest
            {
                EntityId = req.Id,
                Timestamp = DateTime.Now,
                EntityType = EntityType.Table,
                ActiveLogActionType = ActiveLogActionType.Update,
            };
            await _activeLogService.CreateActiveLog(log);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Tables.FindAsync(id);
            if (item == null)
                return 0;

            _context.Tables.Remove(item);

            var log = new ActiveLogCreateRequest
            {
                EntityId = id,
                Timestamp = DateTime.Now,
                EntityType = EntityType.Table,
                ActiveLogActionType = ActiveLogActionType.Delete,
            };
            await _activeLogService.CreateActiveLog(log);

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
            var target = await _context.Tables.Where(x => x.IsActive == true && x.Id == id).FirstOrDefaultAsync();
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
            };

            return item;
        }
    }
}