using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Users;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;
        private readonly LmsDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository,
            IAccountRoleRepository accountRoleRepository,
            IUserRepository userRepository,
            ITokenHandler tokenHandler,
            IEmailHandler emailHandler,
            LmsDbContext bookingDbContext)
        {
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;
            _dbContext = bookingDbContext;
        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var getAccount = (from e in _userRepository.GetAll()
                              join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                              where e.Email == changePasswordDto.Email
                              select a).FirstOrDefault();

            if (getAccount is null)
            {
                return 0; //account not found
            }

            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                OTP = getAccount.OTP,
                ExpiredDate = getAccount.ExpiredDate,
                Password = HashingHandler.GenerateHash(changePasswordDto.Password),
            };

            if (getAccount.OTP != changePasswordDto.OTP)
            {
                return -1; //OTP not same
            }

            if (getAccount.IsUsed == true)
            {
                return -2; //OTP already used
            }

            if (getAccount.ExpiredDate < DateTime.Now)
            {
                return -3; // OTP expired
            }

            _accountRepository.Clear();

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -4; //Account not Update
            }

            return 1;
        }

        public int ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var otp = new Random().Next(111111, 999999);
            var getAccountDetail = (from e in _userRepository.GetAll()
                                    join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                    where e.Email == forgotPasswordDto.Email
                                    select a).FirstOrDefault();

            if (getAccountDetail is null)
            {
                return 0; // no email found
            }

            _accountRepository.Clear();

            var isUpdated = _accountRepository.Update(new Account
            {
                Guid = getAccountDetail.Guid,
                Password = getAccountDetail.Password,
                ExpiredDate = DateTime.Now.AddMinutes(5),
                OTP = otp,
                IsUsed = false,
                CreatedDate = getAccountDetail.CreatedDate,
                ModifiedDate = getAccountDetail.ModifiedDate
            });

            if (!isUpdated)
            {
                return -1; // error update
            }

            //sending email to user mail
            _emailHandler.SendEmail(new EmailMessageDto
            {
                ToEmail = forgotPasswordDto.Email,
                Subject = "Forgot Password",
                Message = $"Your OTP is {otp}." +
                $"<br>" +
                $"<br>" +
                $"Please click the link below to change your passsword" +
                $"<br>" +
                $"<a href='https://localhost:7059/account/change-password'>click here</a>"

            });

            return 1;
        }

        public string Login(LoginDto loginDto)
        {
            try
            {
                var getEmployee = _userRepository.GetByEmail(loginDto.Email);

                if (getEmployee is null)
                {
                    return "0"; // User not found
                }

                var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);

                if (!HashingHandler.ValidateHash(loginDto.Password, getAccount.Password))
                {
                    return "0"; // Login not success
                }

                var getRoles = _accountRoleRepository.GetRoleNamesByAccountGuid(getEmployee.Guid);

                //Claims atau payload berupa isi data yang disimpan token
                var claims = new List<Claim>
                {
                    new Claim("Guid", getAccount.Guid.ToString()),
                    new Claim("FullName", $"{getEmployee.FirstName} {getEmployee.LastName}"),
                    new Claim("Email", getEmployee.Email)
                };

                //role untuk mengecek hak akses dari user
                foreach (var role in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var generatedToken = _tokenHandler.GenerateToken(claims);
                if (generatedToken is null)
                {
                    return "-2"; // token tidak dapat dibuat
                }

                return generatedToken;
            }

            catch
            {
                return "0"; //user tidak ada
            }
        }

        public RegisterDto? Register(RegisterDto registerDto)
        {
            //anticipation If error do rollback
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                //User Create
                var userToCreate = new NewUserDto
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    Gender = registerDto.Gender,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                };

                var userResult = _userRepository.Create(userToCreate);

                //Account Create
                var accountResult = _accountRepository.Create(new NewAccountDto
                {
                    Guid = userResult.Guid,
                    IsUsed = true,
                    ExpiredDate = DateTime.Now.AddMinutes(5),
                    OTP = 111,
                    Password = HashingHandler.GenerateHash(registerDto.Password)
                });

                //AccountRole Create, Otomatis buat menjadi role employee
                var accountRole = _accountRoleRepository.Create(new NewAccountRoleDto
                {
                    AccountGuid = accountResult.Guid,
                    RoleGuid = registerDto.RoleGuid
                });

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return null;
            }

            return (RegisterDto)registerDto;
        }

        public IEnumerable<AccountDto> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return Enumerable.Empty<AccountDto>();
            }

            var accountDtos = new List<AccountDto>();
            foreach (var account in accounts)
            {
                accountDtos.Add((AccountDto)account);
            }

            return accountDtos; // account is found;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var accountDto = _accountRepository.GetByGuid(guid);
            if (accountDto is null)
            {
                return null;
            }

            return (AccountDto)accountDto;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var accountToCreate = newAccountDto;

            //accountToCreate.Password = HashingHandler.GenerateHash(newAccountDto.Password);
            var accountDto = _accountRepository.Create(accountToCreate);
            if (accountDto is null)
            {
                return null;
            }

            return (AccountDto)accountDto;
        }

        public int Update(AccountDto accountDto)
        {
            var account = _accountRepository.GetByGuid(accountDto.Guid);
            if (account is null)
            {
                return -1; // account is null or not found;
            }

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = account.CreatedDate;
            //toUpdate.Password = HashingHandler.GenerateHash(accountDto.Password);
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 // account is updated;
                : 0; // account failed to update;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return -1; // account is null or not found;
            }

            var result = _accountRepository.Delete(account);

            return result ? 1 // account is deleted;
                : 0; // account failed to delete;
        }
    }
}
