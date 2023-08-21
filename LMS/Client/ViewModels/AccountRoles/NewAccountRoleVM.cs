using Client.Models;

namespace Client.ViewModels.AccountRoles;

public class NewAccountRoleVM
{
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(NewAccountRoleVM newAccountRoleVM)
    {
        return new AccountRole
        {
            Guid = new Guid(),
            AccountGuid = newAccountRoleVM.AccountGuid,
            RoleGuid = newAccountRoleVM.RoleGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

    }

    public static explicit operator NewAccountRoleVM(AccountRole accountRole)
    {
        return new NewAccountRoleVM
        {
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid,
        };
    }
}
