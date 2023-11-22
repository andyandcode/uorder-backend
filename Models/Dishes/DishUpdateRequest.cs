using Data.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Models.Dishes
{
    public class DishUpdateRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int CompletionTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int QtyPerDay { get; set; }

        [Required]
        public DishType Type { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }
    }
}