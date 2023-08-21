using API.Models;

namespace API.Contracts
{
    public interface IGradeRepository : IGeneralRepository<Grade>
    {
        bool IsNotExist(Guid userGuid, Guid taskGuid);

        bool IsDataUnique(Guid userGuid, Guid taskGuid);
    }
}
