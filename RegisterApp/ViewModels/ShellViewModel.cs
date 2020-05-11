using Caliburn.Micro;
using DataAccessLib.Models;
using MediatR;
using Newtonsoft.Json;
using RegisterApp.Common;
using RepositoryLib;
using RepositoryLib.Commands;
using RepositoryLib.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RegisterApp.ViewModels
{
    public class ShellViewModel:Conductor<IScreen>.Collection.AllActive
    {
        #region fields
        private readonly IMediator     _mediator;
        private string _cityName;
        private string _firstName;
        private string _minAge;
        private string _maxAge;
        #endregion

        #region props


        public string CityName
        {
            get =>_cityName; 
            set { _cityName = value; NotifyOfPropertyChange(nameof(CityName)); }
        }

        public string FirstName
        {
            get =>_firstName; 
            set { _firstName = value; NotifyOfPropertyChange(nameof(FirstName)); }
        }

        public string MinAge
        {
            get=>_minAge; 
            set { _minAge = value; NotifyOfPropertyChange( ()=>MinAge); }
        }

        public string MaxAge
        {
            get => _maxAge;
            set { _maxAge = value; NotifyOfPropertyChange(() => MaxAge); }
        }
        #endregion

        #region ctor
        public ShellViewModel( IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region funcs
        private void LoadSampleData()
        {
            using var unitOfWork = IoC.Get<IUnitOfWork>();
            if (unitOfWork.Persons.GetCount() != 0)
                return;
            var file = System.IO.File.ReadAllText(AppParams.StrGeneratedJson);
            var people = JsonConvert.DeserializeObject<List<Person>>(file);
            unitOfWork.Persons.AddRange(people);
            unitOfWork.Complete();
        }


        public void OnGetAddressesByCityName()
        {
            if (string.IsNullOrEmpty(CityName))
                return;
            GetAddressesByCityNameAsync();
        }

        private async void GetAddressesByCityNameAsync()
        {
            var addresses = await _mediator.Send(new GetAddressesByCityQuery((CityName)));
            addresses.Reverse();
        }

        public async void OnGetPersonsByFirstName()
        {
            if (string.IsNullOrEmpty(FirstName))
                return;
            await GetAllPersonsByFirstNameAsync();
        }

        private async Task<IEnumerable<Person>> GetAllPersonsByFirstNameAsync()
        {
            var query = new GetPersonsByFirstNameQuery(FirstName);
            IEnumerable<Person> persons = null;
            try
            {
                persons = await _mediator.Send(query);//Task.Factory.StartNew(()=> { var x = new List<Person>(); return x; });
            }
            catch (Exception e)
            {

            }
            return persons;
        }

        public async void OnGetPersonsByAgeRangeAsync()
        {
            do
            {
                if (!int.TryParse(MinAge, out var iMinAge))
                {
                    MessageBox.Show("The min age is not available", "Best Practice", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (!int.TryParse(MaxAge, out var iMaxAge))
                {
                    MessageBox.Show("The max age is not available", "Best Practice", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                var query = new GetPersonsByAgeRangeQuery(iMinAge,iMaxAge);
                IEnumerable<Person> persons = null;
                try
                {
                    persons = await _mediator.Send(query);//Task.Factory.StartNew(()=> { var x = new List<Person>(); return x; });
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Fail to get the persons fitting the age range between {iMinAge}  and {iMaxAge}", "Best Practice", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            } while (false);
            
        }


        public void OnInsertDefaultPerson()
        {
            DoInsertDefaultPerson();
        }

        public async void DoInsertDefaultPerson()
        {
            List<Address> addresses = new List<Address>()
            {
                new Address()
                {
                    StreetAddress = "Lane 887, ZuChongzhi Road", City = "Shanghai", State = "Shanghai",
                    ZipCode = "15740-0001"
                }
            };
            List<Email> emailAddresses = new List<Email>()
            {
                new Email() {EmailAddress = "Toby.Yan@xxx.com"}
            };
            var toby = new Person() { FirstName = "Toby", LastName = "Yan", Age = 28, Weight = 66, Addresses = addresses, EmailAddresses = emailAddresses };
            var command = new AddNewPersonCommand(toby);
            try
            {
                await _mediator.Send(command);
            }
            catch(Exception)
            {

            }
        }

        public async void OnModifyName()
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(1) );
            await _mediator.Send(new ModifyPersonNameCommand(person,"Kobe"));
        }
        #endregion
    }
}
