using DataAccessLib.Models;
using MediatR;
using RepositoryLib.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLib.Handlers
{
    public class GetPersonsByAgeRangeHandler:IRequestHandler<GetPersonsByAgeRangeQuery, IEnumerable<Person>>
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public GetPersonsByAgeRangeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region funcs
        public async Task<IEnumerable<Person>> Handle(GetPersonsByAgeRangeQuery request, CancellationToken cancellationToken)
        {
            using(_unitOfWork)
            {
               return await Task.Run(() => { return _unitOfWork.Persons.GetPersonsByAgeRange(request); });
            }
        }
        #endregion
    }
}
