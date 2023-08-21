using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UserClassroomRepository : GenericRepository<UserClassroom>, IUserClassroomRepository
    {
        public UserClassroomRepository(LmsDbContext context) : base(context)
        {
        }


        public bool IsNotExist(Guid userGuid, Guid classroomGuid)
        {
            return _context.Set<UserClassroom>().FirstOrDefault(uc => uc.UserGuid == userGuid && uc.ClassroomGuid == classroomGuid) is null;
        }
        public bool IsDataUnique(Guid userGuid, Guid classroomGuid)
        {
            var existingData = _context.Set<UserClassroom>().FirstOrDefault(uc => uc.UserGuid == userGuid && uc.ClassroomGuid == classroomGuid);

            if (existingData is null)
            {
                return true;
            }


            bool isSameAsExisitingData = existingData.UserGuid == userGuid && existingData.ClassroomGuid == classroomGuid;

            return isSameAsExisitingData;
        }
    }
}
