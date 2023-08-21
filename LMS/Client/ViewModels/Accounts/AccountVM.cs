using Client.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels.Accounts;

public class AccountVM
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }

    public static implicit operator Account(AccountVM accountDto)
    {
        return new Account
        {
            Guid = accountDto.Guid,
            Password = accountDto.Password,
            OTP = accountDto.OTP,
            IsUsed = accountDto.IsUsed,
            ExpiredDate = accountDto.ExpiredDate,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator AccountVM(Account account)
    {
        return new AccountVM
        {
            Guid = account.Guid,
            Password = account.Password,
            OTP = account.OTP,
            IsUsed = account.IsUsed,
            ExpiredDate = account.ExpiredDate
        };
    }
}
