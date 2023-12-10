namespace Models.Orders
{
    public class CartItemVm
    {
        public int Discount { get; set; }
        public int SubTotal { get; set; }
        public int TotalItems { get; set; }
        public ICollection<CartDishVm> Dishes { get; set; }
    }
}