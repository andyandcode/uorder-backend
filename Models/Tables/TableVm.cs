namespace Models.Tables
{
    public class TableVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Route { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Orders { get; set; } = new List<string>();
    }
}