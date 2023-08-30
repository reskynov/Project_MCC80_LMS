namespace Client.ViewModels.Users
{
    public class DashboardStudentVM
    {
        public int TotalClassroom { get; set; }
        public int TotalAssignment { get; set; }
        public int TotalSubmitted { get; set; }
        public int TotalNotSubmitted { get; set; }
        public IEnumerable<int?> LatestGraded { get; set; }
        public IEnumerable<string?> LatestTaskName { get; set; }

    }
}
