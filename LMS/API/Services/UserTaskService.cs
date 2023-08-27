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

    public UserTaskService(IUserTaskRepository UserTaskRepository, ITaskRepository taskRepository, ILessonRepository lessonRepository)
    {
        _userTaskRepository = UserTaskRepository;
        _taskRepository = taskRepository;
        _lessonRepository = lessonRepository;
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
            Grade = -1,
            TaskGuid = getTask.Guid,
            UserGuid = submitTaskDto.UserGuid
        };

        var UserTask = _userTaskRepository.Create(newUserTask);

        if (UserTask is null)
        {
            return -1;
        }

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
        var UserTask = _userTaskRepository.GetByGuid(guid);
        if (UserTask is null)
        {
            return null;
        }

        return(UserTaskDto)UserTask;
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
}
