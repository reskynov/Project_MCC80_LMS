﻿using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Classrooms;
using API.DTOs.Lessons;
using API.DTOs.Users;
using API.DTOs.UserTasks;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
                                    select new {c, uc, t, ut });

            if (detailAssignment is null)
            {
                return null;
            }

            var classroomAvg = detailAssignment.Where(assign => assign.ut.Grade is not null)
                                               .GroupBy(group => new { group.c?.Name })
                                               .Select(g => new {
                                                   AverageGrade = g.Average(g => g.ut.Grade),
                                                   ClassroomName = g.Key.Name
                                               });

            var dashboard = new DashboardTeacherDto
            {
                TotalClassroom = totalClassroom,
                TotalAssignment = detailAssignment.Where(assign => assign.t is not null).Count(),
                TotalGraded = detailAssignment.Where(assign => assign.ut.Grade is not null).Count(),
                TotalNotGraded = detailAssignment.Where(assign => assign.ut.Grade is null).Count(),
                AverageGrade = classroomAvg.OrderBy(c => c.ClassroomName).Select(c => c.AverageGrade).Take(5).ToList(),
                ClassNameAverageGrade = classroomAvg.OrderBy(c => c.ClassroomName).Select(c => c.ClassroomName).Take(5).ToList(),
                AverageGradePassed = classroomAvg.Where(c => c.AverageGrade >= 70).Count(),
                AverageGradeNotPassed = classroomAvg.Where(c => c.AverageGrade < 70).Count()
            };

            return dashboard;

        }

        public DashboardStudentDto? GetDashboardStudent(Guid guid)
        {
            var totalClassroom = _userClassroomRepository.GetAll().Where(uc => uc.UserGuid == guid).Count();

            var totalTask = from u in _userRepository.GetAll()
                                  join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                  join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                  join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                  join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                  where u.Guid == guid
                                  select new { t };

            var detailAssignment = (from u in _userRepository.GetAll()
                                    join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                    join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                    join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                    join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                    join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid
                                    where u.Guid == guid && ut.UserGuid == guid
                                    select new {l, uc, t, ut});

            if(detailAssignment is null)
            {
                return null;
            }

            var dashboard = new DashboardStudentDto
            {
                TotalClassroom = totalClassroom,
                TotalAssignment = totalTask.Count(),
                TotalSubmitted = detailAssignment.Where(assign => assign.ut.Attachment is not null).Count(),
                TotalGraded = detailAssignment.Where(assign => assign.ut?.Grade is not null).Count(),
                LatestGraded = detailAssignment.Where(assign => assign.ut?.Grade is not null).OrderByDescending(assign => assign.ut?.ModifiedDate).Select(assign => assign.ut?.Grade).Take(5).ToList(),
                LatestTaskName = detailAssignment.Where(assign => assign.ut?.Grade is not null).OrderByDescending(assign => assign.ut?.ModifiedDate).Select(assign => assign.l.Name).Take(5).ToList(),
                GradePassed = detailAssignment.Where(assign => assign.ut?.Grade is not null).Where(grade => grade.ut.Grade >= 70).Count(),
                GradeNotPassed = detailAssignment.Where(assign => assign.ut?.Grade is not null).Where(grade => grade.ut.Grade < 70).Count()
            };

            return dashboard;

        }

        public IEnumerable<TeacherTaskDto> GetTeacherTasks(Guid guid)
        {
            var getStudentTask = from u in _userRepository.GetAll()
                                 join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                                 join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                                 join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                 join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                 where u.Guid == guid
                                 select new TeacherTaskDto
                                 {
                                     ClassroomName = c.Name,
                                     LessonGuid = l.Guid,
                                     LessonName = l.Name,
                                     DeadlineDate = t?.DeadlineDate,
                                     TotalGraded = GetTotalSubmitted(l.Guid).Where(t => t.Grade is not null).Count(),
                                     TotalNotGraded = GetTotalSubmitted(l.Guid).Where(t => t.Grade is null).Count(),
                                     TotalNotSubmitted = GetTotalPeopleClass(c.Guid) - GetSubmittedPeople(c.Guid, l.Guid).Count()
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
                                     Grade = GetSubmittedTask(guid, t.Guid)?.Grade,
                                     IsSubmitted = GetSubmittedTask(guid, t.Guid) is not null
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

        public UserTaskDto? GetSubmittedTask(Guid guidUser, Guid guidTask)
        {
            var result = _userTaskRepository.GetAll()
                        .SingleOrDefault(user => user.TaskGuid == guidTask && user.UserGuid == guidUser);

            if (result is null)
            {
                return null;
            }

            return (UserTaskDto) result;
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
                                         DeadlineDate = t?.DeadlineDate,
                                         TotalGraded = GetTotalSubmitted(l.Guid).Where(t => t.Grade is not null).Count(),
                                         TotalNotGraded = GetTotalSubmitted(l.Guid).Where(t => t.Grade is null).Count(),
                                         TotalNotSubmitted = GetTotalPeopleClass(c.Guid) - GetTotalSubmitted(l.Guid).Count()
                                     };

            if (getClassroomLesson is null)
            {
                return Enumerable.Empty<TeacherTaskInClassDto>(); //classroom lesson not found
            }

            return getClassroomLesson; // classroom lesson is found;
        }

        public IEnumerable<UserTaskDto> GetSubmittedPeople(Guid classroomGuid, Guid lessonGuid)
        {
            var getTotalPeople = from c in _classroomRepository.GetAll()
                                 join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                 where c.Guid == classroomGuid
                                 select new 
                                 {
                                     uc.UserGuid
                                 };

            var getTotalTask = from l in _lessonRepository.GetAll()
                               join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                               join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid
                               join uc in getTotalPeople on ut.UserGuid equals uc.UserGuid
                               where l.Guid == lessonGuid
                               select new UserTaskDto
                               {
                                   Guid = ut.Guid,
                                   Attachment = ut.Attachment,
                                   Grade = ut.Grade,
                                   TaskGuid = ut.TaskGuid,
                                   UserGuid = ut.UserGuid
                               };

            return getTotalTask;
        }

        public IEnumerable<UserTaskDto> GetTotalSubmitted(Guid lessonGuid)
        {
            var getTotalTask = from l in _lessonRepository.GetAll()
                                    join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                    join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid
                                    where l.Guid == lessonGuid
                                    select new UserTaskDto
                                    {
                                        Guid = ut.Guid,
                                        Attachment = ut.Attachment,
                                        Grade = ut.Grade,
                                        TaskGuid = ut.TaskGuid,
                                        UserGuid = ut.UserGuid
                                    };

            return getTotalTask;
        }

        public int GetTotalPeopleClass(Guid classroomGuid)
        {
            var totalPeopleClass = (from c in _classroomRepository.GetAll()
                                    join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                    where c.Guid == classroomGuid
                                    select new { uc }).Count() - 1;

            return totalPeopleClass;
        }
    }
}
