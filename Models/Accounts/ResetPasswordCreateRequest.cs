namespace Models.Accounts
{
    public class ResetPasswordCreateRequest
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}