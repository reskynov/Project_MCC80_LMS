using API.Contracts;
using API.DTOs.Classrooms;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class ClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;

        public ClassroomService(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
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
            if (classroomDto is null)
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
    }
}

