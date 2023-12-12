using Data.Enums;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Utilities.Common;

namespace Models.Dishes
{
    public class DishCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Dish);

        [Required]
        public string Name { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DishType Type { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt => DateTime.Now;

        public IFormFile? Cover { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public static bool IsDeleted => false;
    }
}