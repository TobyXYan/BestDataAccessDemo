using Caliburn.Micro;
using DataAccessLib.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegisterApp.Common;
using RegisterApp.ViewModels;
using RepositoryLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RegisterApp
{
    public class Bootstrapper:BootstrapperBase
    {
        #region fields
        protected readonly SimpleContainer Container;
        private IServiceProvider _serviceProvider;
        private readonly Task _configTask;
        #endregion

        #region ctor
        public Bootstrapper()
        {
            _configTask = new Task(ConfigureServices);
            _configTask.Start();
            Container   = new SimpleContainer();
            Initialize();
        }
        #endregion


        private void  ConfigureServices()
        {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile(AppParams.StrAppSettingJson, false)
                .Build();

                var appAssembly = Assembly.GetExecutingAssembly();
                var repositoryAssembly = Assembly.Load("RepositoryLib");
                var services = new ServiceCollection();

                services.AddMediatR(repositoryAssembly);
              
                services.AddMediatR(appAssembly);
                services.AddTransient(_ => new PeopleContext(new DbContextOptionsBuilder<PeopleContext>().UseSqlServer(configuration.GetConnectionString("PeopleConnection")).Options));
                services.AddTransient<ShellViewModel>();
                services.AddTransient<IUnitOfWork, UnitOfWork>();
                _serviceProvider = services.BuildServiceProvider();
        }


        protected override void Configure()
        {
            Container.Singleton<IEventAggregator, EventAggregator>();
            Container.Singleton<IWindowManager, WindowManager>();
            Container.PerRequest<IUnitOfWork, UnitOfWork>();
            Container.RegisterHandler(typeof(IMediator), null, GetMediator);
            Container.RegisterHandler(typeof(PeopleContext), null, GetPeopleContext);
        }

        private IMediator GetMediator(SimpleContainer container)
        {
            return _serviceProvider.GetRequiredService<IMediator>();
        }

        private PeopleContext GetPeopleContext(SimpleContainer container)
        {
            var context =_serviceProvider.GetService<PeopleContext>();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return context;
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = Container.GetInstance(service,key);
            return instance;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            _configTask.Wait();
            var shellView = _serviceProvider.GetRequiredService<ShellViewModel>();
            var wndManger = IoC.Get<IWindowManager>();
            wndManger.ShowDialog(shellView);
        }

    }
}

