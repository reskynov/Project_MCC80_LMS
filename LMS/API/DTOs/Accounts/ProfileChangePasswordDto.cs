namespace API.DTOs.Accounts
{
    public class ProfileChangePasswordDto
    {
        public Guid GuidAccount { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
