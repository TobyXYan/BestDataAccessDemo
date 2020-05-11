using RepositoryLib.Interfaces;
using System;

namespace RepositoryLib
{
    public interface IUnitOfWork:IDisposable
    {
        IAddressRepository Addresses { get; }
        IEmailRepository EmailAddresses { get; }
        IPersonRepository Persons { get; }
        int Complete();
    }
}
