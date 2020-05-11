using DataAccessLib.Models;
using MediatR;

namespace RepositoryLib.Commands
{
    public class ModifyPersonNameCommand : IRequest
    {
        #region props
        public Person CurPerson { get;}
        public string NewName { get;}
        #endregion

        #region ctor
        public ModifyPersonNameCommand(Person person, string name)
        {
            CurPerson = person;
            NewName   = name;
        }
        #endregion
    }
}
