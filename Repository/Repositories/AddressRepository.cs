using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using RepositoryLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLib.Repositories
{
    public class AddressRepository:Repository<Address>,IAddressRepository
    {
        #region ctor
        public AddressRepository(PeopleContext context):base(context)
        {
        }
        #endregion

        public IEnumerable<Address> GetAddressesByCity(string city)
        {
            return Context.Addresses.Where(c=>c.City == city).ToList();
        }

    }
}
