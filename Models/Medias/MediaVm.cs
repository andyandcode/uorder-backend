using Data.Entities;

namespace Models.Medias
{
    public class MediaVm
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<DishMedia> DishMedias { get; set; }
    }
}