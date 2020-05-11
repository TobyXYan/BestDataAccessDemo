using DataAccessLib.Models;
using MediatR;
using System.Collections.Generic;

namespace RepositoryLib.Queries
{
    public class GetPersonsByFirstNameQuery:IRequest<IEnumerable<Person>>
    {
        #region props
        public string FirstName { get; }
        #endregion

        #region ctor
        public GetPersonsByFirstNameQuery(string firstName)
        {
            FirstName = firstName;
        }
        #endregion
    }
}
