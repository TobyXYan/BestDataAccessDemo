using DataAccessLib.Models;
using MediatR;
using RepositoryLib.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLib.Handlers
{
    public class GetAddressesByCityHandler : IRequestHandler<GetAddressesByCityQuery, IEnumerable<Address>>
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public GetAddressesByCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region funcs
        public async Task<IEnumerable<Address>> Handle(GetAddressesByCityQuery request, CancellationToken cancellationToken)
        {
            using(_unitOfWork)
            {
                return await Task.Run(() => { return _unitOfWork.Addresses.GetAddressesByCity(request.CityName); });
            }
        }
        #endregion
    }
}
