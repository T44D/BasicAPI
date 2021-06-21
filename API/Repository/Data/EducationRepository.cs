using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        public EducationRepository(MyContext context) : base(context) {}
    }
}
