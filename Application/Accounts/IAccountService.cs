using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Models.Accounts;

namespace Application.Accounts
{
    public interface IAccountService
    {
        Task<string> Login(AccountLoginRequest req);

        Task<string> ResetPassword(ResetPasswordCreateRequest req);

        Task<AccountVm> GetAccountByUsername(string username);

        Task<AccountVm> GetAccountById(string username);

        Task<AccountVm> GetAccountByUsernamePrivate(string username);

        Task<AccountVm> GetAccountByIdPrivate(string username);

        Task<string> Create(AccountCreateRequest req);

        Task<int> Update(AccountUpdateRequest req);

        Task<int> Delete(string id);

        Task HardDelete(string itemId);

        Task<int> UndoDelete(string itemId);

        Task<List<AccountVm>> GetAll();

        Task<List<RoleVm>> GetAllRoles();

        Task<int> UpdateStatus(string id, JsonPatchDocument<Account> patchDoc);
    }
}