using DataAccessLib.Models;
using MediatR;

namespace RepositoryLib.Queries
{
    public class GetPersonByIdQuery:IRequest<Person>
    {
        #region props
        public int Id { get; }
        #endregion

        #region ctor
        public GetPersonByIdQuery(int id)
        {
            Id = id;
        }
        #endregion
    }
}
