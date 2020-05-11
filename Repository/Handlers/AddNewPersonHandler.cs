using MediatR;
using RepositoryLib.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLib.Handlers
{
    public class AddNewPersonHandler:IRequestHandler<AddNewPersonCommand>
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public AddNewPersonHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region funcs
        public async Task<Unit> Handle(AddNewPersonCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                await Task.Run(() => _unitOfWork.Persons.Add(request.NewMember));
                _unitOfWork.Complete();
                return Unit.Value;
            }
        }
        #endregion

    }
}
