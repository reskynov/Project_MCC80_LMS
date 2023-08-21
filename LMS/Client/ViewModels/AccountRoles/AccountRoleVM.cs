using Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.AccountRoles;

public class AccountRoleVM
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(AccountRoleVM accountRoleVM)
    {
        return new AccountRole
        {
            Guid = accountRoleVM.Guid,
            AccountGuid = accountRoleVM.AccountGuid,
            RoleGuid = accountRoleVM.RoleGuid,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator AccountRoleVM(AccountRole accountRole)
    {
        return new AccountRoleVM
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
}
