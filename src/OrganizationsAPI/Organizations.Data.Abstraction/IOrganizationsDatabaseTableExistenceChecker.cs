using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction
{
	public interface IOrganizationsDatabaseTableExistenceChecker
	{
		ICollection<string> TableNames { get; }
		bool IsTableExisting(string tableName);
	}
}
