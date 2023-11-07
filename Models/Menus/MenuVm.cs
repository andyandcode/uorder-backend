namespace Models.Menus
{
    public class MenuVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> DishMenus { get; set; } = new List<string>();
    }
}