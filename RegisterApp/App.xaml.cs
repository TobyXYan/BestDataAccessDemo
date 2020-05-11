using Caliburn.Micro;
using DataAccessLib.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegisterApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RegisterApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartUp(object sender, StartupEventArgs e)
        {
        }

        private void ConfigureServices(ref ServiceCollection services)
        {
            services.AddTransient<ShellViewModel>();
        }

        private void AppExit(object sender, ExitEventArgs e)
        {

        }
    }
}
