using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Classrooms;
using API.DTOs.Lessons;
using API.DTOs.Users;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserClassroomRepository _userClassroomRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly ILessonRepository _lessonRepository;

        public UserService(IUserRepository userRepository, 
            IClassroomRepository classroomRepository, 
            IUserClassroomRepository userClassroomRepository,
            IRoleRepository roleRepository,
            IAccountRoleRepository accountRoleRepository,
            IAccountRepository accountRepository,
            ITaskRepository taskRepository,
            IUserTaskRepository userTaskRepository,
            ILessonRepository lessonRepository)
        {
            _userRepository = userRepository;
            _classroomRepository = classroomRepository;
            _userClassroomRepository = userClassroomRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountRepository = accountRepository;
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
            _lessonRepository = lessonRepository;
        }

        public DashboardTeacherDto? GetDashboardTeacher(Guid guid)
        {
            var totalClassroom = _userClassroomRepository.GetAll().Where(uc => uc.UserGuid == guid).Count();

            var detailAssignment = (from u in _userRepository.GetAll()
                                    join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                    join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                    join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                    join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                    join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid 
                                    where u.Guid == guid
                                    select new { uc, t, ut });

            if (detailAssignment is null)
            {
                return null;
            }

            var dashboard = new DashboardTeacherDto
            {
                TotalClassroom = totalClassroom,
                TotalAssignment = detailAssignment.Count(),
                TotalGraded = detailAssignment.Where(assign => assign.ut.Grade is not null).Count(),
                TotalNotGraded = detailAssignment.Where(assign => assign.ut.Grade is null).Count()
            };

            return dashboard;

        }

        public DashboardStudentDto? GetDashboardStudent(Guid guid)
        {
            var totalClassroom = _userClassroomRepository.GetAll().Where(uc => uc.UserGuid == guid).Count();

            var detailAssignment = (from u in _userRepository.GetAll()
                                    join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                    join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                    join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                    join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                    join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid into utj
                                    from ut in utj.DefaultIfEmpty()
                                    where u.Guid == guid
                                    select new {l, uc, t, ut});

            if(detailAssignment is null)
            {
                return null;
            }

            var dashboard = new DashboardStudentDto
            {
                TotalClassroom = totalClassroom,
                TotalAssignment = detailAssignment.Count(),
                TotalSubmitted = detailAssignment.Where(assign => assign.ut is not null).Count(),
                TotalNotSubmitted = detailAssignment.Where(assign => assign.ut is null).Count(),
                LatestGraded = detailAssignment.Where(assign => assign.ut?.Grade is not null).OrderByDescending(assign => assign.ut?.ModifiedDate).Select(assign => assign.ut?.Grade).Take(5).ToList(),
                LatestTaskName = detailAssignment.Where(assign => assign.ut?.Grade is not null).OrderByDescending(assign => assign.ut?.ModifiedDate).Select(assign => assign.l.Name).Take(5).ToList(),
                GradePassed = detailAssignment.Where(assign => assign.ut?.Grade is not null).Where(grade => grade.ut.Grade >= 70).Count(),
                GradeNotPassed = detailAssignment.Where(assign => assign.ut?.Grade is not null).Where(grade => grade.ut.Grade < 70).Count()
            };

            return dashboard;

        }

        public IEnumerable<TeacherTaskInClassDto> GetClassroomTask(Guid guid)
        {
            var getClassroomLesson = from c in _classroomRepository.GetAll()
                                     join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                     join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                     where c.Guid == guid
                                     select new TeacherTaskInClassDto
                                     {
                                         LessonGuid = l.Guid,
                                         LessonName = l.Name,
                                         DeadlineDate = t?.DeadlineDate
                                     };

            if (getClassroomLesson is null)
            {
                return Enumerable.Empty<TeacherTaskInClassDto>(); //classroom lesson not found
            }

            return getClassroomLesson; // classroom lesson is found;
        }

        public IEnumerable<TeacherTaskDto> GetTeacherTasks(Guid guid)
        {
            var getStudentTask = from u in _userRepository.GetAll()
                                 join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                 join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                 where u.Guid == guid
                                 select new TeacherTaskDto
                                 {
                                     ClassroomName = c.Name,
                                     TaskInClassroom = GetClassroomTask(c.Guid)
                                 };

            if (!getStudentTask.Any())
            {
                return Enumerable.Empty<TeacherTaskDto>();
            }

            return getStudentTask;
        }

        public IEnumerable<StudentTaskDto> GetStudentTasks(Guid guid)
        {
            var getStudentTask = from u in _userRepository.GetAll()
                                 join ut in _userTaskRepository.GetAll() on u.Guid equals ut.UserGuid into utj
                                 from ut in utj.DefaultIfEmpty()
                                 join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                 join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                 join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                 join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                 where u.Guid == guid
                                 select new StudentTaskDto
                                     {
                                         ClassroomName = c.Name,
                                         LessonGuid = l.Guid,
                                         LessonName = l.Name,
                                         TaskGuid = t.Guid,
                                         Deadline = t.DeadlineDate,
                                         Grade = ut?.Grade,
                                         IsSubmitted = ut?.Attachment is not null
                                     };

            if (!getStudentTask.Any())
            {
                return Enumerable.Empty<StudentTaskDto>();
            }

            return getStudentTask;
        }

        public int ChangePassword(ProfileChangePasswordDto profileChangePasswordDto)
        {
            var getAccount = (from  a in _accountRepository.GetAll()
                              where a.Guid == profileChangePasswordDto.GuidAccount
                              select a).FirstOrDefault();

            if (getAccount is null)
            {
                return 0; //account not found
            }

            if(!HashingHandler.ValidateHash(profileChangePasswordDto.CurrentPassword, getAccount.Password))
            {
                return -1;
            }

            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                OTP = getAccount.OTP,
                ExpiredDate = getAccount.ExpiredDate,
                Password = HashingHandler.GenerateHash(profileChangePasswordDto.NewPassword),
            };

            _accountRepository.Clear();

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -2; //Account not Update
            }

            return 1;
        }

        public ProfileDto GetProfile(Guid guid)
        {
            var getUser = (from u in _userRepository.GetAll()
                                     where u.Guid == guid
                                     select new ProfileDto
                                     {
                                         Guid = u.Guid,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         BirthDate = u.BirthDate,
                                         Gender = u.Gender,
                                         Email = u.Email,
                                         PhoneNumber = u.PhoneNumber
                                     }).SingleOrDefault();

            if (getUser is null)
            {
                return null;
            }

            return getUser;
        }

        public IEnumerable<ClassroomByUserDto> GetCLassroomByUser(Guid guid)
        {
            var getClassroomByUser = from c in _classroomRepository.GetAll()
                                     join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                     join u in _userRepository.GetAll() on uc.UserGuid equals u.Guid
                                     join t in _userRepository.GetAll() on c.TeacherGuid equals t.Guid
                                     where u.Guid == guid
                                     select new ClassroomByUserDto
                                     {
                                         ClassroomGuid = c.Guid,
                                         ClassroomName = c.Name,
                                         TeacherName = t.FirstName + " " + t.LastName,
                                         UserClassroomGuid = uc.Guid,
                                         StudentCount = _userClassroomRepository.GetAll().Where(uc => uc.ClassroomGuid == c.Guid && uc.UserGuid != guid).Count()
                                     };

            if (!getClassroomByUser.Any())
            {
                return Enumerable.Empty<ClassroomByUserDto>();
            }

            return getClassroomByUser;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            if (!users.Any())
            {
                return Enumerable.Empty<UserDto>();
            }

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add((UserDto)user);
            }

            return userDtos;
        }

        public UserDto? GetByGuid(Guid guid)
        {
            var user = _userRepository.GetByGuid(guid);
            if (user is null)
            {
                return null;
            }

            return (UserDto)user;
        }

        public UserDto? Create(NewUserDto newUserDto)
        {
            var user = _userRepository.Create(newUserDto);
            if (user is null)
            {
                return null;
            }

            return (UserDto)user;
        }

        public int Update(UserDto userDto)
        {
            var user = _userRepository.GetByGuid(userDto.Guid);

            if (user is null)
            {
                return -1;
            }

            User toUpdate = userDto;
            toUpdate.CreatedDate = user.CreatedDate;

            var result = _userRepository.Update(toUpdate);
            return result ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var user = _userRepository.GetByGuid(guid);

            if (user is null)
            {
                return -1;
            }

            var result = _userRepository.Delete(user);
            return result ? 1 : 0;
        }
    }
}
