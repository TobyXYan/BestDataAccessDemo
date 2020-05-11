using DataAccessLib.Models;
using RepositoryLib.Queries;
using System.Collections.Generic;

namespace RepositoryLib.Interfaces
{
    public interface IPersonRepository:IRepository<Person>
    {
        IEnumerable<Person> GetPersonsByFirstName(string name);
        IEnumerable<Person> GetPersonsByAgeRange(GetPersonsByAgeRangeQuery request);
        void AddWithCollection(Person newPerson);
        int GetCount();

    }
}
