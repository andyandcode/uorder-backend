using Data.Enums;

namespace Data.Entities
{
    public class Dish
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public int CompletionTime { get; set; }
        public bool IsActive { get; set; }
        public int QtyPerDate { get; set; }
        public DishType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<DishMedia> DishMedias { get; set; }
        public List<DishMenu> DishMenus { get; set; }
    }
}