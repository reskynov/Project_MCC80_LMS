using API.Contracts;
using API.Data;
using API.Models;
using Newtonsoft.Json.Linq;

namespace API.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(LmsDbContext context) : base(context) { }

        public bool IsExist(Guid guid)
        {
            //return true if not null
            return _context.Set<Role>()
                           .SingleOrDefault(r => r.Guid.Equals(guid)) is not null;
        }
    }
}