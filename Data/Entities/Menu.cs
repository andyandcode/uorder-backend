namespace Data.Entities
{
    public class Menu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<DishMenu> Dishes { get; set; }
        public bool IsDeleted { get; set; }
    }
}