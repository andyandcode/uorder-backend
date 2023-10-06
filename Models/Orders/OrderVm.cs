using Data.Entities;
using Data.Enums;

namespace Models.Orders
{
    public class OrderVm
    {
        public string Id { get; set; }
        public int Total { get; set; }
        public string Note { get; set; }
        public string TableId { get; set; }
        public string TableName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string OrderStatusKey { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> OrderDetails { get; set; } = new List<string>();
        public Table Table { get; set; } = new Table();
    }
}