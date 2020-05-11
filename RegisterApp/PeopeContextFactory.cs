using DataAccessLib.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RegisterApp.Common;
using System;
using System.IO;

namespace RegisterApp
{
    /// <summary>
    /// This factory class derived from IDesignTimeDbContextFactory is used to generate tables in Database from code
    /// Without this, it will fail to find the connection string defined in appsettings.json, and will fail to create/Modify table
    /// </summary>
    public class PeopeContextFactory:IDesignTimeDbContextFactory<PeopleContext>
    {
        #region fields  
        private IConfigurationRoot _configuration;
        #endregion

        #region funcs
        public PeopleContext CreateDbContext(string[] args)
        {
            GetConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<PeopleContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PeopleConnection"));
            return new PeopleContext(optionsBuilder.Options);
        }
      
        private void GetConfiguration()
        {
            _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
           .AddJsonFile(AppParams.StrAppSettingJson, false)
           .Build();
        }
        #endregion

    }
}
