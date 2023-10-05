namespace Data.Entities
{
    public class Table
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<Order> Orders { get; set; }
    }
}