namespace Client.ViewModels.Users
{
    public class ClassroomByUserVM
    {
        public Guid ClassroomGuid { get; set; }
        public string ClassroomName { get; set; }
        public IEnumerable<string>? ClassroomTeacher { get; set; }
    }
}
