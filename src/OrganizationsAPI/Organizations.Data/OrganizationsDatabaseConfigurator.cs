using Organizations.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data
{
	public class OrganizationsDatabaseConfigurator : IOrganizationsDatabaseConfigurator
	{
		private readonly IOrganizationsDatabaseConnectionValidator _connectiongValidator;
		private readonly IOrganizationsDatabaseTableExistenceChecker _tableExistenceChecker;
		private readonly IOrganizationsDatabaseTableInitializer _tableInitializer;

		public OrganizationsDatabaseConfigurator(IOrganizationsDatabaseConnectionValidator connectiongValidator,
												IOrganizationsDatabaseTableExistenceChecker tableExistenceChecker,
												IOrganizationsDatabaseTableInitializer tableInitializer)
		{
			_connectiongValidator = connectiongValidator;
			_tableExistenceChecker = tableExistenceChecker;
			_tableInitializer = tableInitializer;
		}
		public void ConfigureDatabase()
		{
			try
			{
				bool isConnectionValid = _connectiongValidator.IsConnectionValid();

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
