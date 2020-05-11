using DataAccessLib.DataAccess;
using RepositoryLib.Interfaces;
using RepositoryLib.Repositories;
using System;

namespace RepositoryLib
{
    public class UnitOfWork:IUnitOfWork
    {
        #region fields
        private readonly PeopleContext _context;
        #endregion


        #region props
        public IAddressRepository Addresses { get; }
        public IEmailRepository EmailAddresses { get; }
        public IPersonRepository Persons { get; }
        #endregion

        #region ctor
        public UnitOfWork(PeopleContext context)
        {
            _context       = context;
            Addresses      = new AddressRepository(context);
            EmailAddresses = new EmailRepository(context);
            Persons        = new PersonRepository(context);
        }
        #endregion

        #region funcs
        public int Complete()
        {
            var ret = 0;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ret = -1;
                }
            }
            return ret;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
