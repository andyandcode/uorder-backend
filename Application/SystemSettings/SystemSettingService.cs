using Data.EF;
using Data.Entities;
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
            try
            {
                var item = new SystemSetting()
                {
                    Id = req.Id,
                    ChefCount = req.ChefCount,
                    Domain = req.Domain,
                };
                _context.Add(item);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(SystemSettingUpdateRequest req)
        {
            try
            {
                var item = new SystemSetting()
                {
                    Id = req.Id,
                    ChefCount = req.ChefCount,
                    Domain = req.Domain,
                };
                _context.Update(item);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return await _context.SaveChangesAsync();
        }
    }
}