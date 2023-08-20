using API.Models;

namespace API.Contracts
{
    public interface IUserClassroomRepository : IGeneralRepository<UserClassroom>
    {
        bool IsNotExist(Guid userGuid, Guid classroomGuid);
    }
}
