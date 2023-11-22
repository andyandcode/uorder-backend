using System.ComponentModel.DataAnnotations;

namespace Models.OrderDetails
{
    public class OrderDetailsCreateRequest
    {
        public string? OrderId { get; set; }

        [Required]
        public string? DishId { get; set; }

        public string? DishName { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public int Amount { get; set; }

        public string? DishNote { get; set; }
    }
}