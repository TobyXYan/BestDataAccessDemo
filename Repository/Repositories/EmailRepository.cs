using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using RepositoryLib.Interfaces;

namespace RepositoryLib.Repositories
{
    public class EmailRepository : Repository<Email>, IEmailRepository
    {
        #region ctor
        public EmailRepository(PeopleContext context) : base(context)
        {
        }
        #endregion
    }
}
