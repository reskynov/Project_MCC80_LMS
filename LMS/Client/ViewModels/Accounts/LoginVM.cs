using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels.Accounts
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
