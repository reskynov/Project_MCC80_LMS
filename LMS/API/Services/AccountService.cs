﻿using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Users;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly LmsDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository,
            IAccountRoleRepository accountRoleRepository,
            LmsDbContext bookingDbContext)
        {
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _dbContext = bookingDbContext;
        }

        public RegisterDto? Register(RegisterDto registerDto)
        {
            //anticipation If error do rollback
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                //Employee Create
                User employeeToCreate = new NewUserDto
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    Gender = registerDto.Gender,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                };

                //Account Create
                var accountResult = _accountRepository.Create(new NewAccountDto
                {
                    Guid = employeeToCreate.Guid,
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
