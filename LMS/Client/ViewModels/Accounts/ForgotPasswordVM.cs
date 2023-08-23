using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Accounts
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
