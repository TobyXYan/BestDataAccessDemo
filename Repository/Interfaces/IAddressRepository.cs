using DataAccessLib.Models;
using System.Collections.Generic;

namespace RepositoryLib.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        IEnumerable<Address> GetAddressesByCity(string city);
    }
}
