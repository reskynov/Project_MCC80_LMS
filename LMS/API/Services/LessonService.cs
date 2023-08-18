using API.Contracts;
using API.DTOs.Lessons;
using API.Models;

namespace API.Services;

public class LessonService
{
    private readonly ILessonRepository _lessonRepository;

    public LessonService(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
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
