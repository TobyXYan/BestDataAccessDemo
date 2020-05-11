using DataAccessLib.Models;
using MediatR;
using System.Collections.Generic;

namespace RepositoryLib.Queries
{
    public class GetPersonsByAgeRangeQuery:IRequest<IEnumerable<Person>>
    {
        #region props
        public int MinAge { get; }
        public int MaxAge { get; }
        #endregion

        #region ctor
        public GetPersonsByAgeRangeQuery(int minAge, int maxAge)
        {
            MinAge = minAge;
            MaxAge = maxAge;
        }
        #endregion
    }
}
