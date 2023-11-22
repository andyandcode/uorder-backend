namespace Data.Entities
{
    public class Account
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleId { get; set; }
        public Role Roles { get; set; }
    }
}