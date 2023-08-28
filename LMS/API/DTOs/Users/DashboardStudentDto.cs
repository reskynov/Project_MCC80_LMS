namespace API.DTOs.Users
{
    public class DashboardStudentDto
    {
        public int TotalClassroom { get; set; }
        public int TotalAssignment { get; set; }
        public int TotalSubmitted { get; set; }
        public int TotalNotSubmitted { get; set; }

    }
}
