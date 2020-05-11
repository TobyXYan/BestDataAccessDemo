using DataAccessLib.Models;
using MediatR;

namespace RepositoryLib.Commands
{
    public class AddNewPersonCommand:IRequest
    {
        #region props
        public Person NewMember { get; set; }
        #endregion

        #region ctor
        public AddNewPersonCommand(Person person)
        {
            NewMember = person;
        }
        #endregion
    }
}
