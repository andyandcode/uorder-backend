using Data.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Models.Orders
{
    public class OrderUpdateRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public string Note { get; set; }

        [Required]
        public string TableId { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CompletedAt { get; set; }

        public int MoneyChange { get; set; }
        public int MoneyReceive { get; set; }
        public string? Staff { get; set; }
    }
}