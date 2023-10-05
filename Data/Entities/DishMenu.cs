namespace Data.Entities
{
    public class DishMenu
    {
        public string DishId { get; set; }
        public string MenuId { get; set; }
        public Dish Dish { get; set; }
        public Menu Menu { get; set; }
    }
}