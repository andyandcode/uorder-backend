using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Models.Accounts
{
    public class AccountUpdateRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string RoleId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public static bool IsDeleted { get; set; }
    }
}