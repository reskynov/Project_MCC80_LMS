using API.Contracts;
using API.DTOs.Lessons;
using API.DTOs.Tasks;
using API.Models;

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

        var toUpdate = new LessonDto
        {
            Guid = lesson.Guid,
            ClassroomGuid = lesson.ClassroomGuid,
            CreatedDate = lesson.CreatedDate,
            IsTask = lesson.IsTask,
            Name = updateLessonTaskDto.Name,
            Description = updateLessonTaskDto.Description,  
            SubjectAttachment = updateLessonTaskDto.SubjectAttachment
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
