using Data.EF;
using Microsoft.EntityFrameworkCore;
using Models.Tables;

namespace Application.Actions
{
    public class ActionService : IActionService
    {
        private readonly UOrderDbContext _context;

        public ActionService(UOrderDbContext uOrderDbContext)
        {
            _context = uOrderDbContext;
        }

        public async Task<TableVm> CallStaff(string tableId)
        {
            var target = await _context.Tables.Where(x => x.IsActive == true && x.Id == tableId).FirstOrDefaultAsync();
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