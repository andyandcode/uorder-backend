using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Models.Tables
{
    public class TableUpdateRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public static bool IsDeleted { get; set; }
    }
}