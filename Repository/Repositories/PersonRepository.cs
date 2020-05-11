using DataAccessLib.DataAccess;
using DataAccessLib.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using RepositoryLib.Queries;

namespace RepositoryLib.Repositories
{
    public class PersonRepository:Repository<Person>,IPersonRepository
    {
        #region ctor
        public PersonRepository(PeopleContext context):base(context)
        {
        }
        #endregion

        #region funcs
        public IEnumerable<Person> GetPersonsByFirstName(string name)
        {
            //return Context.People.Include(a => a.Addresses).Where(x => x.FirstName == name).ToList();
            //return Context.People.Include(a=>a.Addresses).Where(x => x.FirstName == name).ToList();
            var eQuery = Context.People.Where(p => true);
            eQuery = AttachAddresses(eQuery);
            return eQuery.Where(p=>p.FirstName == name).ToList();
        }

        public IEnumerable<Person> GetPersonsByAgeRange(GetPersonsByAgeRangeQuery request)
        {
            var eQuery = Context.People.Where(p => true);
            eQuery = ApplyMinAgeFilter(eQuery, request);
            eQuery = ApplyMaxAgeFilter(eQuery, request);
            return eQuery.Select(p=>p).ToList();
        }

        public void AddWithCollection(Person newPerson)
        {
            Context.People.Add(newPerson);
        }

        public int GetCount()
        {
            return Context.People.Count();
        }
        #endregion


        #region filters
        private IQueryable<Person> ApplyMinAgeFilter(IQueryable<Person> query, GetPersonsByAgeRangeQuery request)
        {
            return query.Where(p => p.Age >= request.MinAge);
        }

        private IQueryable<Person> ApplyMaxAgeFilter(IQueryable<Person> query, GetPersonsByAgeRangeQuery request)
        {
            return query.Where(p => p.Age < request.MaxAge);
        }

        private IQueryable<Person> AttachAddresses(IQueryable<Person> query)
        {
            return query.Include(p=>p.Addresses);
        }
        private IQueryable<Person> AttachEmail(IQueryable<Person> query)
        {
            return query.Include(p => p.EmailAddresses);
        }
        #endregion
    }
}
