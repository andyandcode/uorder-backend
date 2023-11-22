namespace Data.Entities
{
    public class OrderDetail
    {
        public string OrderId { get; set; }
        public string DishId { get; set; }
        public string DishName { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
        public int Amount { get; set; }
        public string? DishNote { get; set; }
        public Order Order { get; set; }
        public Dish Dish { get; set; }
    }
}