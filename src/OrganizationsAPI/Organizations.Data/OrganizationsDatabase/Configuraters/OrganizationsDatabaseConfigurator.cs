using Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.OrganizationsDatabase.Configuraters
{
    public class OrganizationsDatabaseConfigurator : IOrganizationsDatabaseConfigurator
    {
        private readonly IOrganizationsDatabaseExistenceChecker _databaseExistenceChecker;
        private readonly IOrganizationsDatabaseInitializer _databaseInitializer;
        private readonly IOrganizationsDatabaseConnectionValidator _connectionValidator;
        private readonly IOrganizationsDatabaseTableExistenceChecker _tableExistenceChecker;
        private readonly IOrganizationsDatabaseTableInitializer _tableInitializer;

        public OrganizationsDatabaseConfigurator(IOrganizationsDatabaseExistenceChecker databaseExistenceChecker,
												 IOrganizationsDatabaseInitializer databaseInitializer,
												 IOrganizationsDatabaseConnectionValidator connectionValidator,
                                                 IOrganizationsDatabaseTableExistenceChecker tableExistenceChecker,
                                                 IOrganizationsDatabaseTableInitializer tableInitializer)
        {
            _databaseExistenceChecker = databaseExistenceChecker;
            _databaseInitializer = databaseInitializer;
			_connectionValidator = connectionValidator;
            _tableExistenceChecker = tableExistenceChecker;
            _tableInitializer = tableInitializer;
        }
        public void ConfigureDatabase()
        {
            try
            {
                bool isDatabaseExisting = _databaseExistenceChecker.IsDatabaseExisting();
                if (!isDatabaseExisting) 
                {
					_databaseInitializer.Initialize();
				}

                bool isConnectionValid = _connectionValidator.IsConnectionValid();
                if (isConnectionValid)
                {
					var tableNames = _tableExistenceChecker.TableNames;
					foreach (var table in tableNames)
					{
						bool isTableExisting = _tableExistenceChecker.IsTableExisting(table);
						if (!isTableExisting)
						{
							_tableInitializer.CreateTable(table);
						}
					}
				}

			}
            catch (Exception)
            {
                throw;
            }
        }

    }
}
