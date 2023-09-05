using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Tasks;
using API.DTOs.UserTasks;
using API.Models;

namespace API.Services;

public class UserTaskService
{
    private readonly IUserTaskRepository _userTaskRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IUserClassroomRepository _userClassroomRepository;

    public UserTaskService(IUserTaskRepository UserTaskRepository,
        ITaskRepository taskRepository, 
        ILessonRepository lessonRepository, 
        IUserRepository userRepository, 
        IClassroomRepository classroomRepository, 
        IUserClassroomRepository userClassroomRepository)
    {
        _userTaskRepository = UserTaskRepository;
        _taskRepository = taskRepository;
        _lessonRepository = lessonRepository;
        _userRepository = userRepository;
        _classroomRepository = classroomRepository;
        _userClassroomRepository = userClassroomRepository;
    }

    public int GradeTask(GradeTaskDto gradeTaskDto)
    {
        var userTask = _userTaskRepository.GetByGuid(gradeTaskDto.UserTaskGuid);

        if (userTask is null)
        {
            return -1;
        }

        UserTask toUpdate = userTask;
        toUpdate.Grade = gradeTaskDto.Grade;
        toUpdate.ModifiedDate = DateTime.Now;

        var result = _userTaskRepository.Update(toUpdate);
        return result ? 1 : 0;
    }

    public GetTaskStudentDto? GetGradeTaskStudent(Guid guid)
    {
        var getTaskStudent = (from ut in _userTaskRepository.GetAll()
                              join u in _userRepository.GetAll() on ut.UserGuid equals u.Guid
                             where ut.Guid == guid
                             select new GetTaskStudentDto
                             {
                                 UserTaskGuid = ut.Guid,
                                 StudentName = u.FirstName + " " + u.LastName,
                                 Attachment = ut?.Attachment,
                                 Grade = ut?.Grade
                             }).FirstOrDefault();

        if (getTaskStudent is null)
        {
            return null;
        }

        return getTaskStudent;
    }

    public IEnumerable<GetTaskToGradeDto> GetTaskToGrade(Guid guid)
    {
        var getTaskToGrade = from u in _userRepository.GetAll()
                             join uc in _userClassroomRepository.GetAll() on u.Guid equals uc.UserGuid
                             join c in _classroomRepository.GetAll() on uc.ClassroomGuid equals c.Guid
                             join l in _lessonRepository.GetAll() on c.Guid equals l.ClassroomGuid
                             join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                             where l.Guid == guid && u.Guid != c.TeacherGuid
                             select new GetTaskToGradeDto
                             {
                                 LessonGuid = l.Guid,
                                 UserTaskGuid = GetSubmittedTask(u.Guid, t.Guid)?.Guid,
                                 LessonName = l.Name,
                                 StudentName = u.FirstName + " " + u.LastName,
                                 Grade = GetSubmittedTask(u.Guid, t.Guid)?.Grade,
                                 SubmittedTask = GetSubmittedTask(u.Guid, t.Guid)?.Attachment,
                                 DeadlineDate = t?.DeadlineDate,
                                 SubmittedTaskDate = GetSubmittedTask(u.Guid, t.Guid)?.ModifiedDate,
                                 IsSubmitted = GetSubmittedTask(u.Guid, t.Guid) is not null
                             };

        if(!getTaskToGrade.Any()) 
        {
            return Enumerable.Empty<GetTaskToGradeDto>();
        }

        return getTaskToGrade;
    }

    public int EditSubmittedTask(SubmitTaskDto submitTaskDto)
    {
        var getSubmittedTask = (from l in _lessonRepository.GetAll()
                                join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                                join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid
                                where l.Guid == submitTaskDto.LessonGuid && ut.UserGuid == submitTaskDto.UserGuid
                                select new GetSubmittedTaskDto
                                {
                                    UserTaskGuid = ut.Guid,
                                    Attachment = ut.Attachment,
                                    Grade = ut.Grade,
                                    UserGuid = ut.UserGuid,
                                    TaskGuid = ut.TaskGuid
                                }).FirstOrDefault();

        if (getSubmittedTask is null)
        {
            return -1; //submitted task not found
        }

        var toUpdate = new UserTaskDto
        {
            Guid = getSubmittedTask.UserTaskGuid,
            Attachment = submitTaskDto.Attachment,
            Grade = getSubmittedTask.Grade,
            TaskGuid = getSubmittedTask.TaskGuid,
            UserGuid = getSubmittedTask.UserGuid,
        };

        return Update(toUpdate);
    }

    public GetSubmittedTaskDto? GetSubmittedTask(FindSubmittedTaskDto findSubmittedTaskDto)
    {
        var getSubmittedTask = (from l in _lessonRepository.GetAll()
                       join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                       join ut in _userTaskRepository.GetAll() on t.Guid equals ut.TaskGuid
                       where l.Guid == findSubmittedTaskDto.LessonGuid && ut.UserGuid == findSubmittedTaskDto.UserGuid
                       select new GetSubmittedTaskDto
                       {
                           UserTaskGuid = ut.Guid,
                           Attachment = ut.Attachment,
                           Grade = ut.Grade,
                           UserGuid = ut.UserGuid,
                           TaskGuid = ut.TaskGuid,
                           SubmittedDate = ut.ModifiedDate
                       }).FirstOrDefault();

        if (getSubmittedTask is null)
        {
            return null;
        }

        return getSubmittedTask;
    }

    public int SubmitTask(SubmitTaskDto submitTaskDto)
    {
        var getTask = (from l in _lessonRepository.GetAll()
                          join t in _taskRepository.GetAll() on l.Guid equals t.LessonGuid
                          where l.Guid == submitTaskDto.LessonGuid
                          select t).FirstOrDefault();

        if (getTask is null)
        {
            return 0;
        }

        var newUserTask = new NewUserTaskDto
        {
            Attachment = submitTaskDto.Attachment,
            Grade = null,
            TaskGuid = getTask.Guid,
            UserGuid = submitTaskDto.UserGuid
        };

        var UserTask = Create(newUserTask);

        return 1;
    }

    public IEnumerable<UserTaskDto> GetAll()
    {
        var UserTasks = _userTaskRepository.GetAll();
        if(!UserTasks.Any())
        {
            return Enumerable.Empty<UserTaskDto>();
        }

        var UserTaskDtos = new List<UserTaskDto>();
        foreach(var UserTask in UserTasks)
        {
            UserTaskDtos.Add((UserTaskDto)UserTask);
        }

        return UserTaskDtos;
    }

    public UserTaskDto? GetByGuid(Guid guid)
    {
        var userTask = _userTaskRepository.GetByGuid(guid);
        if (userTask is null)
        {
            return null;
        }

        return(UserTaskDto)userTask;
    }

    public UserTaskDto? Create(NewUserTaskDto newUserTaskDto)
    {
        var UserTask = _userTaskRepository.Create(newUserTaskDto);

        if (UserTask is null)
        {
            return null;
        }

        return (UserTaskDto)UserTask;
    }

    public int Update (UserTaskDto UserTaskDto)
    {
        var UserTask = _userTaskRepository.GetByGuid(UserTaskDto.Guid);

        if (UserTask is null)
        {
            return -1;
        }

        UserTask toUpdate = UserTaskDto;
        toUpdate.CreatedDate = UserTask.CreatedDate;

        var result = _userTaskRepository.Update(toUpdate);
        return result ? 1 : 0;
    }

    public int Delete (Guid guid)
    {
        var UserTask = _userTaskRepository.GetByGuid(guid);
        if (UserTask is null)
        {
            return -1;
        }

        var result = _userTaskRepository.Delete(UserTask);
        return result ? 1 : 0;
    }

    public UserTask? GetSubmittedTask(Guid guidUser, Guid guidTask)
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
