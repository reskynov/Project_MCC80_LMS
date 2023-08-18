using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(LmsDbContext context) : base(context)
        {
        }
    }
}
