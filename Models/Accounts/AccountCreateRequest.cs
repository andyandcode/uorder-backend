using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Utilities.Common;

namespace Models.Accounts
{
    public class AccountCreateRequest
    {
        private readonly IdGeneration item = new IdGeneration();

        [SwaggerSchema(ReadOnly = true)]
        public string Id => item.Generator(GenerationType.Account);

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt => DateTime.Now;

        [Required]
        public string RoleId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public static bool IsDeleted => false;
    }
}