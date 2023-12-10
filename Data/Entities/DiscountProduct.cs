namespace Data.Entities
{
    public class DiscountProduct
    {
        public string DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }
        public string DishId { get; set; }
        public Dish Dish { get; set; }
    }
}