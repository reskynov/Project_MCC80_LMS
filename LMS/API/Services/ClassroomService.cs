using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Classrooms;
using API.DTOs.Lessons;
using API.DTOs.UserClassrooms;
using API.DTOs.Users;
using API.DTOs.UserTasks;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Services
{
    public class ClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserClassroomRepository _userClassroomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public ClassroomService(IClassroomRepository classroomRepository, 
            IUserClassroomRepository userClassroomRepository, 
            IUserRepository userRepository, 
            IAccountRoleRepository accountRoleRepository,
            IRoleRepository roleRepository,
            ILessonRepository lessonRepository,
            ITaskRepository taskRepository,
            IUserTaskRepository userTaskRepository)
        {
            _classroomRepository = classroomRepository;
            _userClassroomRepository = userClassroomRepository;
            _userRepository = userRepository;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
            _lessonRepository = lessonRepository;
            _taskRepository = taskRepository;
            _userTaskRepository = userTaskRepository;
        }

        public int CreateNewClassCode(Guid guid)
        {
            var classroom = _classroomRepository.GetByGuid(guid);
            if (classroom is null)
            {
                return -1; // classroom is null or not found;
            }

            Classroom toUpdate = classroom;
            toUpdate.Code = GenerateHandler.ClassCode();
            toUpdate.ExpiredDate = DateTime.Now.AddDays(1);
            var result = _classroomRepository.Update(toUpdate);

            return result ? 1 // classroom is updated;
                : 0; // classroom failed to update;
        }

        public IEnumerable<ClassroomLessonDto> GetClassroomLessons(Guid guidClass, Guid guidUser)
        {
            var getClassroomLesson = from c in _classroomRepository.GetAll()
                                     join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                     join u in _userRepository.GetAll() on uc.UserGuid equals u.Guid
                                     join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                                     join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid into lt
                                     from t in lt.DefaultIfEmpty()
                                     where u.Guid == guidUser && c.Guid == guidClass
                                     select new ClassroomLessonDto
                                     {
                                         ClassroomGuid = c.Guid,
                                         LessonGuid = l.Guid,
                                         Name = l.Name,
                                         Description = l.Description,
                                         isTask = l.IsTask,
                                         CreatedDate = l.CreatedDate,
                                         SubjectAttachment = l.SubjectAttachment,
                                         DeadlineDate = t?.DeadlineDate,
                                         StudentSubmittedDate = t?.Guid is not null ? GetSubmittedTask(guidUser, t?.Guid)?.ModifiedDate : null
                                     };

            if (getClassroomLesson is null)
            {
                return Enumerable.Empty<ClassroomLessonDto>(); //classroom lesson not found
            }

            return getClassroomLesson; // classroom lesson is found;
        }

        public IEnumerable<ClassroomPeopleDto> GetClassroomPeoples(Guid guid) 
        {
            var getClassroomPeople = from c in _classroomRepository.GetAll()
                                     join uc in _userClassroomRepository.GetAll() on c.Guid equals uc.ClassroomGuid
                                     join u in _userRepository.GetAll() on uc.UserGuid equals u.Guid
                                     join ar in _accountRoleRepository.GetAll() on u.Guid equals ar.AccountGuid
                                     join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                     where c.Guid == guid
                                     select new ClassroomPeopleDto
                                     {
                                         UserGuid = u.Guid,
                                         FullName = u.FirstName + " " + u.LastName,
                                         Role = r.Name,
                                         UserClassroomGuid = uc.Guid
                                     };

            if (getClassroomPeople is null)
            {
                return Enumerable.Empty<ClassroomPeopleDto>(); //classroom people not found
            }

            return getClassroomPeople; // classroom is found;
        }

        public ClassroomByCodeDto? GetEnrollClassroomByCode(string classCode)
        {
            var getClassroom = (from c in _classroomRepository.GetAll()
                               join t in _userRepository.GetAll() on c.TeacherGuid equals t.Guid
                               where c.Code == classCode && c.ExpiredDate > DateTime.Now
                               select new ClassroomByCodeDto
                               {
                                   ClassroomGuid = c.Guid,
                                   ClassroomName = c.Name,
                                   TeacherName = t.FirstName + " " + t.LastName,
                                   PeopleCount = _userClassroomRepository.GetAll().Where(uc => uc.ClassroomGuid == c.Guid).Count()
                               }).SingleOrDefault();

            if (getClassroom is null)
            {
                return null; //classroom not found
            }

            return getClassroom;
        }

        public int EnrollClassroom(EnrollClassroomDto enrollClassroomDto)
        {
            var getClassroom = (from c in _classroomRepository.GetAll()
                              where c.Code == enrollClassroomDto.ClassroomCode
                              select c).SingleOrDefault();

            if (getClassroom is null)
            {
                return 0; //classroom not found
            }

            if (DateTime.Now > getClassroom.ExpiredDate)
            {
                return -1; //classroom code already expired
            }

            var userClassroomToCreate = new NewUserClassroomDto {
                ClassroomGuid = getClassroom.Guid,
                UserGuid = enrollClassroomDto.UserGuid
            };

            var valueNotExist =  _userClassroomRepository.IsNotExist(userClassroomToCreate.UserGuid, userClassroomToCreate.ClassroomGuid);
            if (!valueNotExist)
            {
                return -2; //already enrolled
            }

            var enrollResult = _userClassroomRepository.Create(userClassroomToCreate);

            if(enrollResult is null)
            {
                return -3; //error when create
            }

            return 1;
        }

        public IEnumerable<ClassroomDto> GetAll()
        {
            var classrooms = _classroomRepository.GetAll();
            if (!classrooms.Any())
            {
                return Enumerable.Empty<ClassroomDto>();
            }

            var classroomDtos = new List<ClassroomDto>();
            foreach (var classroom in classrooms)
            {
                classroomDtos.Add((ClassroomDto)classroom);
            }

            return classroomDtos; // classroom is found;
        }

        public ClassroomDto? GetByGuid(Guid guid)
        {
            var classroomDto = _classroomRepository.GetByGuid(guid);
            if (classroomDto is null)
            {
                return null;
            }

            return (ClassroomDto)classroomDto;
        }

        public ClassroomDto? Create(NewClassroomDto newClassroomDto)
        {
            var classroomDto = _classroomRepository.Create(newClassroomDto);
            var userclassroomDto = _userClassroomRepository.Create(new NewUserClassroomDto
            {
                ClassroomGuid = classroomDto.Guid,
                UserGuid = classroomDto.TeacherGuid
            });
            if (classroomDto is null && userclassroomDto is null)
            {
                return null;
            }

            return (ClassroomDto)classroomDto;
        }

        public int Update(ClassroomDto classroomDto)
        {
            var classroom = _classroomRepository.GetByGuid(classroomDto.Guid);
            if (classroom is null)
            {
                return -1; // classroom is null or not found;
            }

            Classroom toUpdate = classroomDto;
            toUpdate.CreatedDate = classroom.CreatedDate;
            var result = _classroomRepository.Update(toUpdate);

            return result ? 1 // classroom is updated;
                : 0; // classroom failed to update;
        }

        public int Delete(Guid guid)
        {
            var classroom = _classroomRepository.GetByGuid(guid);
            if (classroom is null)
            {
                return -1; // classroom is null or not found;
            }

            var result = _classroomRepository.Delete(classroom);

            return result ? 1 // classroom is deleted;
                : 0; // classroom failed to delete;
        }

        public UserTask? GetSubmittedTask(Guid guidUser, Guid? guidTask)
        {
            var result = _userTaskRepository.GetAll()
                        .SingleOrDefault(user => user.TaskGuid == guidTask && user.UserGuid == guidUser);

            if (result is null)
            {
                return null;
            }

            return result;
        }
    }
}

