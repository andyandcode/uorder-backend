using Data.Enums;
using Models.Menus;

namespace Models.Dishes
{
    public class DishVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public DishType Type { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> OrderDetails { get; set; } = new List<string>();
        public virtual List<MenuVm> DishMenus { get; set; }
        public string? CoverLink { get; set; }
    }
}