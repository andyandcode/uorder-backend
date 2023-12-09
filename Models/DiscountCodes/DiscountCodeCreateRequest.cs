using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Utilities.Common;

namespace Models.DiscountCodes
{
    public class DiscountCodeCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Discount);

        [Required]
        public string Code { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public int Percentage { get; set; }

        public int MaxDiscountAmount { get; set; }
        public int MinDiscountAmount { get; set; }
        public int MinOrderAmountRequired { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool AppliesToAllProducts { get; set; }

        public ICollection<string>? ApplicableProductIds { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt => DateTime.Now;
    }
}