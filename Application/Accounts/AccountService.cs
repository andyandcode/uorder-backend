using Application.ActiveLogs;
using Application.Jwt;
using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Accounts;
using Utilities.Common;
using Utilities.Constants;

namespace Application.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly UOrderDbContext _context;
        private readonly IActiveLogService _activeLogService;
        private readonly IJwtService _jwtService;

        public AccountService(UOrderDbContext dbContext, IActiveLogService activeLogService, IJwtService jwtService)
        {
            _context = dbContext;
            _activeLogService = activeLogService;
            _jwtService = jwtService;
        }

        public async Task<string> Login(AccountLoginRequest req)
        {
            var checkExist = await GetAccountByUsername(req.Username);
            if (checkExist != null)
            {
                if (checkExist.IsActive == false)
                {
                    return SystemConstants.AccountLocked;
                }
                if (HandleHashes.VerifyPwd(req.Password, checkExist.Password))
                {
                    return _jwtService.GenerateToken(checkExist);
                }
                return SystemConstants.WrongPassword;
            }
            return SystemConstants.NotFoundAccount;
        }

        public async Task<string> ResetPassword(ResetPasswordCreateRequest req)
        {
            var acc = await GetAccountById(req.Id);
            if (acc == null)
            {
                return SystemConstants.NotFoundAccount;
            }

            if (acc.IsActive == false)
            {
                return SystemConstants.AccountLocked;
            }

            if (req.NewPassword == null || req.OldPassword == null)
            {
                return SystemConstants.PasswordsNotNull;
            }

            if (!HandleHashes.VerifyPwd(req.OldPassword, acc.Password))
            {
                return SystemConstants.PasswordsNotMatch;
            }

            var item = new Account
            {
                Id = acc.Id,
                IsActive = acc.IsActive,
                Username = acc.Username,
                Password = HandleHashes.EndcodePwd(req.NewPassword),
                CreatedAt = acc.CreatedAt,
            };
            _context.Update(item);

            await _context.SaveChangesAsync();
            return SystemConstants.ResetSuccessfully;
        }

        public async Task<AccountVm> GetAccountByUsername(string username)
        {
            var account = await _context.Accounts.Where(x => x.Username == username).Select(x => new AccountVm
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                CreatedAt = x.CreatedAt,
                IsActive = x.IsActive,
                RoleId = x.RoleId,
                RoleName = x.Roles.Name
            }).SingleOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

            return account;
        }

        public async Task<AccountVm> GetAccountById(string id)
        {
            var account = await _context.Accounts.Where(x => x.Id == id).Select(x => new AccountVm
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                CreatedAt = x.CreatedAt,
                IsActive = x.IsActive,
                RoleId = x.RoleId,
                RoleName = x.Roles.Name
            }).SingleOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

            return account;
        }
    }
}