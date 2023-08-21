﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(LmsDbContext context) : base(context)
        {
        }


        public bool IsNotExist(Guid userGuid, Guid taskGuid)
        {
            return _context.Set<Grade>()
                .FirstOrDefault(g => g.UserGuid == userGuid && g.TaskGuid == taskGuid) is null;
        }
        public bool IsDataUnique(Guid userGuid, Guid taskGuid)
        {
            var existingData = _context.Set<Grade>()
                .FirstOrDefault(g => g.UserGuid == userGuid && g.TaskGuid == taskGuid);

            if (existingData is null)
            {
                return true;
            }
            
            
            bool isSameAsExisitingData = existingData.UserGuid == userGuid && existingData.TaskGuid == taskGuid;

            return isSameAsExisitingData;
        }
    }
}
