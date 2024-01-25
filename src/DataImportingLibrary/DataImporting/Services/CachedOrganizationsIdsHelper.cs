using DataImporting.Abstraction.Services;
using Organizations.Data.Abstraction.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class CachedOrganizationsIdsHelper : ICachedOrganizationsIdsHelper
	{
		private HashSet<string> _organizationsIdsSet;

		public void AddId(string id)
		{
			_organizationsIdsSet.Add(id);
		}

		public bool ContainsId(string id)
		{
			return _organizationsIdsSet.Contains(id);
		}

		public void LoadData(IOrganizationsContext organizationsContext)
		{
			_organizationsIdsSet = organizationsContext.Organizations.GetAllIds().ToHashSet();
		}

		public void RemoveId(string id)
		{
			_organizationsIdsSet.Remove(id);
		}
	}
}
