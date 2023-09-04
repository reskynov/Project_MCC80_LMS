namespace Client.ViewModels.Classrooms
{
    public class ClassroomByCodeVM
    {
        public Guid ClassroomGuid { get; set; }
        public string ClassroomName { get; set; }
        public string TeacherName { get; set; }
        public int PeopleCount { get; set; }
    }
}
