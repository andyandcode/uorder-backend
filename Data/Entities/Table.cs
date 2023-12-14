namespace Data.Entities
{
    public class Table
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public bool IsDeleted { get; set; }
    }
}