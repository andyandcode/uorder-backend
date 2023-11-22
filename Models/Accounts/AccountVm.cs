namespace Models.Accounts
{
    public class AccountVm
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}