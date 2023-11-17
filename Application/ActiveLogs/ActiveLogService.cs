using Data.EF;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Models.ActiveLogs;

namespace Application.ActiveLogs
{
    public class ActiveLogService : IActiveLogService
    {
        private readonly UOrderDbContext _context;

        public ActiveLogService(UOrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> CreateActiveLog(ActiveLogCreateRequest req)
        {
            var item = new ActiveLog()
            {
                Id = req.Id,
                EntityId = req.EntityId,
                Timestamp = req.Timestamp,
                EntityType = req.EntityType,
                ActiveLogActionType = req.ActiveLogActionType,
            };
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ActiveLogVm>> GetActiveLogByEntityId(string id)
        {
            var result = await _context.ActiveLogs.Where(x => x.EntityId == id).Select(x => new ActiveLogVm()
            {
                Key = x.Id,
                Id = x.Id,
                EntityId = x.EntityId,
                Timestamp = x.Timestamp,
                EntityType = x.EntityType,
                ActiveLogActionType = x.ActiveLogActionType,
            }).ToListAsync();
            var list = result.OrderByDescending(x => x.Timestamp).ToList();

            return list;
        }

        public async Task<List<ActiveLogVm>> GetActiveLogByEntityType(EntityType type)
        {
            var result = await _context.ActiveLogs.Where(x => x.EntityType == type).Select(x => new ActiveLogVm()
            {
                Key = x.Id,
                Id = x.Id,
                EntityId = x.EntityId,
                Timestamp = x.Timestamp,
                EntityType = x.EntityType,
                ActiveLogActionType = x.ActiveLogActionType,
            }).ToListAsync();
            var list = result.OrderByDescending(x => x.Timestamp).ToList();

            return list;
        }
    }
}