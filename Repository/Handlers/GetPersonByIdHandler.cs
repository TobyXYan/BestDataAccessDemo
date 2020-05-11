using DataAccessLib.Models;
using MediatR;
using RepositoryLib.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLib.Handlers
{
    public class GetPersonByIdHandler:IRequestHandler<GetPersonByIdQuery, Person>
    {
        #region fileds
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public GetPersonByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region funcs
        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            using(_unitOfWork)
            {
                return await Task.Run(()=>_unitOfWork.Persons.Get(request.Id));
            }
        }
        #endregion
    }
}
