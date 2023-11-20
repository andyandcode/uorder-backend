using System.ComponentModel.DataAnnotations;

namespace Models.Accounts
{
    public class AccountLoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}