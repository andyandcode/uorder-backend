using Application.SystemSettings;
using Data.EF;
using Data.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Models.Tables;
using Utilities.Constants;

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
            var item = new Table()
            {
                Id = req.Id,
                Name = req.Name,
                IsActive = req.IsActive,
                Desc = req.Desc,
                CreatedAt = req.CreatedAt,
            };
            _context.Add(item);

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

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var itemToDelete = await _context.Tables.FindAsync(id);

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
            var itemToHardDelete = await _context.Tables.FindAsync(itemId);

            if (itemToHardDelete != null && itemToHardDelete.IsDeleted)
            {
                _context.Tables.Remove(itemToHardDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UndoDelete(string itemId)
        {
            var itemToUndo = await _context.Tables.FindAsync(itemId);

            if (itemToUndo != null)
            {
                itemToUndo.IsDeleted = false;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<List<TableVm>> GetAll()
        {
            var setting = await _systemSettingService.GetSettings();
            return _context.Tables.Where(x => x.IsDeleted == false).ToList().Select(p => new TableVm()
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