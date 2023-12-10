using Data.Entities;
using Data.Enums;
using Models.OrderDetails;

namespace Models.Orders
{
    public class OrderVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Note { get; set; }
        public string TableId { get; set; }
        public string TableName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderStatusKey { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<OrderDetailsVm> OrderDetails { get; set; }
        public OrderType OrderType { get; set; }
        public int Subtotal { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public DateTime CompletedAt { get; set; }
        public int TimeToReceive { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int MoneyChange { get; set; }
        public int MoneyReceive { get; set; }
        public string? Staff { get; set; }
        public DiscountCode? DiscountCode { get; set; }
    }
}