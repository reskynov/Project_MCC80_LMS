using Client.Contracts;
using Client.Models;

namespace Client.Repositories
{
    public class GradeRepository : GeneralRepository<Grade, Guid>, IGradeRepository
    {
        public GradeRepository(string request = "grades/") : base(request)
        {
        }
    }
}
