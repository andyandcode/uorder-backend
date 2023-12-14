using Data.Enums;

namespace Data.Entities
{
    public class Dish
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
        public DishType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Cover { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<DishMenu> Menus { get; set; }
        public virtual ICollection<DiscountProduct> HasDiscountCodes { get; set; }
        public bool IsDeleted { get; set; }
    }
}