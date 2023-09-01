using API.Contracts;
using API.DTOs.Lessons;
using API.DTOs.Tasks;
using API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Services;

public class LessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ITaskRepository _taskRepository;

    public LessonService(ILessonRepository lessonRepository, ITaskRepository taskRepository)
    {
        _lessonRepository = lessonRepository;
        _taskRepository = taskRepository;
    }

    public LessonDetailDto? GetLessonDetailByGuid(Guid guid)
    {
        var lesson = _lessonRepository.GetByGuid(guid);
        if (lesson is null)
        {
            return null;
        }

        var lessonToGet = new LessonDetailDto
        {
            ClassroomGuid = lesson.ClassroomGuid,
            Name = lesson.Name,
            Description = lesson.Description,
            CreatedDate = lesson.CreatedDate,
            Guid = lesson.Guid,
            IsTask = lesson.IsTask,
            SubjectAttachment = lesson.SubjectAttachment,
            DeadlineDate = null
        };

        if(lesson.IsTask)
        {
            var task = (from u in _taskRepository.GetAll()
                        where u.LessonGuid == guid
                        select u).FirstOrDefault();

            if (task is null)
            {
                return null; // task is null
            }
            lessonToGet.DeadlineDate = task.DeadlineDate;
        }

        return lessonToGet;
    }

    public UpdateLessonTaskDto? GetLessonTaskByGuid(Guid guid)
    {
        var lesson = _lessonRepository.GetByGuid(guid);

        if (lesson is null)
        {
            return null;
        }

        var toUpdate = new UpdateLessonTaskDto
        {
            LessonGuid = lesson.Guid,
            Name = lesson.Name,
            Description = lesson.Description,
            SubjectAttachment = lesson.SubjectAttachment
        };

        if (lesson.IsTask) // cek apakah lesson berupa task
        {
            var task = (from u in _taskRepository.GetAll()
                        where u.LessonGuid == guid
                        select u).FirstOrDefault();

            if (task is null)
            {
                return null; // task is null
            }
            toUpdate.DeadlineDate = task.DeadlineDate;
        }

        return toUpdate;
    }

    public int UpdateLessonTask(UpdateLessonTaskDto updateLessonTaskDto)
    {
        var lesson = _lessonRepository.GetByGuid(updateLessonTaskDto.LessonGuid);

        if (lesson is null)
        {
            return -1;
        }

        if (lesson.IsTask) // cek apakah lesson berupa task
        {
            var task = (from u in _taskRepository.GetAll()
                        where u.LessonGuid == updateLessonTaskDto.LessonGuid
                        select u).FirstOrDefault();

            if (task is null)
            {
                return -2; // task is null
            }

            task.DeadlineDate = updateLessonTaskDto.DeadlineDate;

            var resultUpdateTask = _taskRepository.Update(task);
            if(resultUpdateTask is false)
            {
                return -3; // update task failed
            }
        }

        var toUpdate = new Lesson
        {
            Guid = lesson.Guid,
            ClassroomGuid = lesson.ClassroomGuid,
            CreatedDate = lesson.CreatedDate,
            IsTask = lesson.IsTask,
            Name = updateLessonTaskDto.Name,
            Description = updateLessonTaskDto.Description,  
            SubjectAttachment = updateLessonTaskDto.SubjectAttachment,
            ModifiedDate = lesson.ModifiedDate,
        };

        var result = _lessonRepository.Update(toUpdate);

        return result ? 1 : 0;
    }

    public LessonDto? CreateLessonTask(NewLessonTaskDto newLessonTaskDto)
    {
        var lesson = new NewLessonDto
        {
            Name = newLessonTaskDto.Name,
            Description = newLessonTaskDto.Description,
            SubjectAttachment = newLessonTaskDto.SubjectAttachment,
            IsTask = newLessonTaskDto.IsTask,
            ClassroomGuid = newLessonTaskDto.ClassroomGuid
        };
        var resultLesson = _lessonRepository.Create(lesson);
        if (resultLesson is null)
        {
            return null; //buat lesson gagal
        }

        if(newLessonTaskDto.IsTask is true )
        {
            var task = new NewTaskDto
            {
                DeadlineDate = newLessonTaskDto.DeadlineDate,
                LessonGuid = resultLesson.Guid
            };
            var resultTask = _taskRepository.Create(task);
            if (resultTask is null) 
            {
                return null; //buat task gagal
            }
        }

        return (LessonDto)resultLesson;
    }

    public IEnumerable<LessonDto> GetAll()
    {
        var lessons = _lessonRepository.GetAll();
        if (!lessons.Any())
        {
            return Enumerable.Empty<LessonDto>();
        }

        var lessonDtos = new List<LessonDto>();
        foreach (var lesson in lessons)
        {
            lessonDtos.Add((LessonDto) lesson);
        }

        return lessonDtos;
    }

    public LessonDto? GetByGuid(Guid guid)
    {
        var lesson = _lessonRepository.GetByGuid(guid);
        if (lesson is null)
        {
            return null;
        }

        return (LessonDto)lesson;
    }

    public LessonDto? Create(NewLessonDto newLessonDto)
    {
        var lesson = _lessonRepository.Create(newLessonDto);
        if (lesson is null)
        {
            return null;
        }

        return (LessonDto)lesson;
    }

    public int Update (LessonDto lessonDto)
    {
        var lesson = _lessonRepository.GetByGuid(lessonDto.Guid);

        if (lesson is null)
        {
            return -1;
        }

        Lesson toUpdate = lessonDto;
        toUpdate.CreatedDate = lesson.CreatedDate;

        var result = _lessonRepository.Update(toUpdate);
        return result ? 1 : 0;
    }

    public int Delete (Guid guid)
    {
        var lesson = _lessonRepository.GetByGuid(guid);

        if (lesson is null)
        {
            return -1;
        }

        var result = _lessonRepository.Delete(lesson);
        return result ? 1 : 0;
    }
}
