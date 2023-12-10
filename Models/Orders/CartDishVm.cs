namespace Models.Orders
{
    public class CartDishVm
    {
        public string DishId { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Qty { get; set; }
    }
}