namespace Data.Entities
{
    public class Media
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<DishMedia> DishMedias { get; set; }
    }
}