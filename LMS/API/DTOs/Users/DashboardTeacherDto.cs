namespace API.DTOs.Users
{
    public class DashboardTeacherDto
    {
        public int TotalClassroom { get; set; }
        public int TotalAssignment { get; set; }
        public int TotalGraded { get; set; }
        public int TotalNotGraded { get; set; }
    }
}
