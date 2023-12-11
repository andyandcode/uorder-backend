using Application.Jwt;
using AutoMapper;
using Data.EF;
using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models.Accounts;
using Utilities.Common;
using Utilities.Constants;

namespace Application.Accounts
{
    public class AccountService : Hub, IAccountService
    {
        private readonly UOrderDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountService(UOrderDbContext dbContext, IJwtService jwtService, IMapper mapper)
        {
            _context = dbContext;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<string> Login(AccountLoginRequest req)
        {
            var checkExist = await GetAccountByUsernamePrivate(req.Username);

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
            var acc = await GetAccountByIdPrivate(req.Id);
            if (acc == null)
            {
                return SystemConstants.NotFoundAccount;
            }

            if (acc.IsActive == false)
            {
                return SystemConstants.AccountLocked;
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
                RoleId = acc.RoleId
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

        public async Task<AccountVm> GetAccountByUsernamePrivate(string username)
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
                Key = x.Id,
                Id = x.Id,
                Username = x.Username,
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

        public async Task<AccountVm> GetAccountByIdPrivate(string id)
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

        public async Task<string> Create(AccountCreateRequest req)
        {
            var accounts = await GetAll();
            bool isTaken = accounts.Exists(x => x.Username.Equals(req.Username));
            if (isTaken == true)
            {
                return SystemConstants.UsernameExists;
            }
            var item = new Account
            {
                Id = req.Id,
                CreatedAt = req.CreatedAt,
                Username = req.Username,
                Password = HandleHashes.EndcodePwd(req.Password),
                IsActive = req.IsActive,
                RoleId = req.RoleId,
            };
            _context.Add(item);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<int> Update(AccountUpdateRequest req)
        {
            var checkExist = await GetAccountByUsernamePrivate(req.Username);

            var item = new Account
            {
                Id = req.Id,
                CreatedAt = req.CreatedAt,
                Username = req.Username,
                Password = HandleHashes.EndcodePwd(req.Password),
                IsActive = req.IsActive,
                RoleId = req.RoleId,
            };
            _context.Update(item);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var product = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<AccountVm>> GetAll()
        {
            return await _context.Accounts.Select(p => new AccountVm()
            {
                Key = p.Id,
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Username = p.Username,
                IsActive = p.IsActive,
                RoleId = p.RoleId,
                RoleName = p.Roles.Name
            }).ToListAsync();
        }

        public async Task<List<RoleVm>> GetAllRoles()
        {
            return await _context.Roles.Select(p => new RoleVm()
            {
                Key = p.Id,
                Id = p.Id,
                Name = p.Name,
                Level = p.Level,
            }).ToListAsync();
        }

        public async Task<int> UpdateStatus(string id, JsonPatchDocument<Account> patchDoc)
        {
            var stockItem = await GetAccountByIdPrivate(id);
            var userDto = _mapper.Map<Account>(stockItem);
            patchDoc.ApplyTo(userDto);
            _context.Update(userDto);

            return await _context.SaveChangesAsync();
        }
    }
}