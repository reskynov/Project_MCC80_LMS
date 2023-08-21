using Client.Models;

namespace Client.ViewModels.Accounts;

public class NewAccountVM
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }

    public static implicit operator Account(NewAccountVM newAccountDto)
    {
        return new Account
        {
            Guid = newAccountDto.Guid,
            Password = newAccountDto.Password,
            OTP = newAccountDto.OTP,
            IsUsed = newAccountDto.IsUsed,
            ExpiredDate = newAccountDto.ExpiredDate,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}
