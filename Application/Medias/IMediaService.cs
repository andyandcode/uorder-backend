using Models.Medias;

namespace Application.Medias
{
    public interface IMediaService
    {
        Task<int> Create(MediaCreateRequest req);

        Task<int> Delete(string id);

        List<MediaVm> GetAll();

        Task<MediaVm> GetById(string id);
    }
}