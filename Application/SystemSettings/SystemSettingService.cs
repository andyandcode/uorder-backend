using Application.ActiveLogs;
using Data.EF;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Models.ActiveLogs;
using Models.SystemSettings;

namespace Application.SystemSettings
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly UOrderDbContext _context;
        private readonly IActiveLogService _activeLogService;

        public SystemSettingService(UOrderDbContext dbContext, IActiveLogService activeLogService)
        {
            _context = dbContext;
            _activeLogService = activeLogService;
        }

        public async Task<int> Create(SystemSettingCreateRequest req)
        {
            var item = new SystemSetting()
            {
                Id = req.Id,
                Domain = req.Domain,
            };
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(SystemSettingUpdateRequest req)
        {
            var item = new SystemSetting()
            {
                Id = req.Id,
                Domain = req.Domain,
            };
            _context.Update(item);

            var log = new ActiveLogCreateRequest
            {
                EntityId = req.Id,
                Timestamp = DateTime.Now,
                EntityType = EntityType.SystemSetting,
                ActiveLogActionType = ActiveLogActionType.Update,
            };
            await _activeLogService.CreateActiveLog(log);

            return await _context.SaveChangesAsync();
        }

        public async Task<SystemSettingVm> GetSettings()
        {
            return await _context.SystemSettings.Select(p => new SystemSettingVm()
            {
                Id = p.Id,
                Domain = p.Domain,
            }).FirstAsync();
        }
    }
}