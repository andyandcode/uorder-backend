using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.SystemSettings;

namespace Application.SystemSettings
{
    public class SystemSettingService : ISystemSettingService
    {
        private readonly UOrderDbContext _context;

        public SystemSettingService(UOrderDbContext dbContext)
        {
            _context = dbContext;
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