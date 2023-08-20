namespace API.DTOs.Accounts
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int OTP { get; set; }
    }
}
