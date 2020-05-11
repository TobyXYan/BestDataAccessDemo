using DataAccessLib.Models;
using MediatR;
using System.Collections.Generic;

namespace RepositoryLib.Queries
{
    public class GetAddressesByCityQuery:IRequest<IEnumerable<Address>>
    {
        #region props
        public string CityName { get; }
        #endregion

        #region ctor
        public GetAddressesByCityQuery(string cityName)
        {
            CityName = cityName;
        }
        #endregion

    }
}
