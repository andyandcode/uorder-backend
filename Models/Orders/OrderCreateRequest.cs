using Data.Enums;
using Models.OrderDetails;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Utilities.Common;

namespace Models.Orders
{
    public class OrderCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Menu);

        [Required]
        public int Total { get; set; }

        [Required]
        public int Subtotal { get; set; }

        public int Discount { get; set; }

        public string? Note { get; set; }

        public string? TableId { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt => DateTime.Now;

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public List<OrderDetailsCreateRequest> OrderDetails { get; set; }
    }
}