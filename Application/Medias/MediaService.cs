using Data.EF;
using Data.Entities;
using Models.Medias;

namespace Application.Medias
{
    public class MediaService : IMediaService
    {
        private readonly UOrderDbContext _context;

        public MediaService(UOrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> Create(MediaCreateRequest req)
        {
            var item = new Media()
            {
                Id = req.Id,
                Desc = req.Desc,
                Path = req.Path,
                CreatedAt = req.CreatedAt,
            };
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var item = await _context.Medias.FindAsync(id);
            if (item == null)
                return 0;

            _context.Medias.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public List<MediaVm> GetAll()
        {
            return _context.Medias.ToList().Select(p => new MediaVm()
            {
                Id = p.Id,
                Desc = p.Desc,
                Path = p.Path,
                CreatedAt = p.CreatedAt,
            }).ToList();
        }

        public async Task<MediaVm> GetById(string id)
        {
            var target = await _context.Medias.FindAsync(id);

            var item = new MediaVm()
            {
                Id = target.Id,
                Path = target.Path,
                Desc = target.Desc,
                CreatedAt = target.CreatedAt,
            };

            return item;
        }
    }
}