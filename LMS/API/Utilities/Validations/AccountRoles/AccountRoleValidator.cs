﻿using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class AccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        public AccountRoleValidator()
        {
            RuleFor(acc => acc.AccountGuid)
                .NotEmpty();

            RuleFor(acc => acc.RoleGuid)
                .NotEmpty();
        }
    }
}
