using Models.Accounts;

namespace Application.Accounts
{
    public interface IAccountService
    {
        Task<string> Login(AccountLoginRequest req);

        Task<string> ResetPassword(ResetPasswordCreateRequest req);

        Task<AccountVm> GetAccountByUsername(string username);

        Task<AccountVm> GetAccountById(string username);
    }
}