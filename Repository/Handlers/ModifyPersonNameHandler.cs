using MediatR;
using RepositoryLib.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLib.Handlers
{
    public class ModifyPersonNameHandler:IRequestHandler<ModifyPersonNameCommand>
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ctor
        public ModifyPersonNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region funcs
        public async Task<Unit> Handle(ModifyPersonNameCommand request, CancellationToken cancellationToken)
        {
            using(_unitOfWork)
            {
               await Task.Run(()=>_unitOfWork.Persons.Modify(request.CurPerson)); //Attach the entity first
                request.CurPerson.FirstName = request.NewName;//Modify the property, meanwhile, the property would be marked as changed
                _unitOfWork.Complete();
            }
            return Unit.Value;
        }
        #endregion
    }
}
