using Data.EF;
using Data.Entities;
using Models.Tables;

namespace Application.Tables
{
    public class TableService : ITableService
    {
        private readonly UOrderDbContext _context;

        public TableService(UOrderDbContext dbContext)
        {
            _context = dbContext;
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
                Route = "",
                Data = "",
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
                Route = "",
                Data = "",
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

        public List<TableVm> GetAll()
        {
            return _context.Tables.ToList().Select(p => new TableVm()
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                Data = p.Data,
                Route = p.Route
            }).ToList();
        }

        public async Task<TableVm> GetById(string id)
        {
            var target = await _context.Tables.FindAsync(id);
            if (target == null)
                return null;

            var item = new TableVm()
            {
                Id = target.Id,
                Name = target.Name,
                IsActive = target.IsActive,
                Desc = target.Desc,
                CreatedAt = target.CreatedAt,
                Data = target.Data,
                Route = target.Route,
            };

            return item;
        }
    }
}