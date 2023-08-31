namespace API.DTOs.Users
{
    public class DashboardStudentDto
    {
        public int TotalClassroom { get; set; }
        public int TotalAssignment { get; set; }
        public int TotalSubmitted { get; set; }
        public int TotalGraded { get; set; }
        public List<int?> LatestGraded { get; set; }
        public List<string?> LatestTaskName { get; set; }
        public int GradePassed { get; set; }
        public int GradeNotPassed { get; set; }

    }
}
