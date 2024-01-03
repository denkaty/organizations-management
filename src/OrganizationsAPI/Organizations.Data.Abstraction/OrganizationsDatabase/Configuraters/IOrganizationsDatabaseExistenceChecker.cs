using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.OrganizationsDatabase.Configuraters
{
	public interface IOrganizationsDatabaseExistenceChecker
	{
		bool IsDatabaseExisting();
	}
}
