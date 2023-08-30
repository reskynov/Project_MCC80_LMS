using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Accounts;
using Client.ViewModels.Users;
using System.Collections;

namespace Client.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, Guid>
    {
        public Task<ResponseHandler<IEnumerable<ClassroomByUserVM>>> GetClassroomByUser(Guid guid);
        public Task<ResponseHandler<ProfileVM>> GetProfile(Guid guid);
        public Task<ResponseHandler<ProfileChangePasswordVM>> ProfileChangePassword(ProfileChangePasswordVM entity);
        public Task<ResponseHandler<IEnumerable<StudentTaskVM>>> GetStudentTask(Guid guid);
        public Task<ResponseHandler<DashboardStudentVM>> DashboardStudent(Guid guid);
        public Task<ResponseHandler<DashboardTeacherVM>> DashboardTeacher(Guid guid);
        public Task<ResponseHandler<IEnumerable<TeacherTaskVM>>> GetTeacherTask(Guid guid);
    }
}
