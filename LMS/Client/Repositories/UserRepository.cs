using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Accounts;
using Client.ViewModels.Users;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    public class UserRepository : GeneralRepository<User, Guid>, IUserRepository
    {
        public UserRepository(string request = "users/") : base(request)
        {
        }

        public async Task<ResponseHandler<DashboardTeacherVM>> DashboardTeacher(Guid guid)
        {
            ResponseHandler<DashboardTeacherVM> dashboardTeacher = null;
            using (var response = await httpClient.GetAsync(request + "teacher-dashboard?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                dashboardTeacher = JsonConvert.DeserializeObject<ResponseHandler<DashboardTeacherVM>>(apiResponse);
            }
            return dashboardTeacher;
        }

        public async Task<ResponseHandler<DashboardStudentVM>> DashboardStudent(Guid guid)
        {
            ResponseHandler<DashboardStudentVM> dashboardStudent = null;
            using (var response = await httpClient.GetAsync(request + "student-dashboard?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                dashboardStudent = JsonConvert.DeserializeObject<ResponseHandler<DashboardStudentVM>>(apiResponse);
            }
            return dashboardStudent;
        }

        public async Task<ResponseHandler<IEnumerable<ClassroomByUserVM>>> GetClassroomByUser(Guid guid)
        {
            ResponseHandler<IEnumerable<ClassroomByUserVM>> classroomByUser = null;
            using (var response = await httpClient.GetAsync(request + "classroom?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                classroomByUser = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<ClassroomByUserVM>>>(apiResponse);
            }
            return classroomByUser;
        }

        public async Task<ResponseHandler<ProfileVM>> GetProfile(Guid guid)
        {
            ResponseHandler<ProfileVM> user = null;
            using (var response = await httpClient.GetAsync(request + "profile?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<ResponseHandler<ProfileVM>>(apiResponse);
            }
            return user;
        }

        public async Task<ResponseHandler<ProfileChangePasswordVM>> ProfileChangePassword(ProfileChangePasswordVM entity)
        {
            ResponseHandler<ProfileChangePasswordVM> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request + "profile-change-password", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<ProfileChangePasswordVM>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseHandler<IEnumerable<StudentTaskVM>>> GetStudentTask(Guid guid)
        {
            ResponseHandler<IEnumerable<StudentTaskVM>> studentTask = null;
            using (var response = await httpClient.GetAsync(request + "student-task?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                studentTask = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<StudentTaskVM>>>(apiResponse);
            }
            return studentTask;
        }
    }
}
