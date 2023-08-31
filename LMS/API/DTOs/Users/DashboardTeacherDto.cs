namespace API.DTOs.Users
{
    public class DashboardTeacherDto
    {
        public int TotalClassroom { get; set; }
        public int TotalAssignment { get; set; }
        public int TotalGraded { get; set; }
        public int TotalNotGraded { get; set; }
        public List<double?> AverageGrade { get; set; }
        public List<string?> ClassNameAverageGrade { get; set; }
        public int AverageGradePassed { get; set; }
        public int AverageGradeNotPassed { get; set; }
    }
}
