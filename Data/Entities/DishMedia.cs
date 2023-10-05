namespace Data.Entities
{
    public class DishMedia
    {
        public string DishId { get; set; }
        public string MediaId { get; set; }
        public Dish Dish { get; set; }
        public Media Media { get; set; }
    }
}