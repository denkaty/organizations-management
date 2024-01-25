using Organizations.Data.Abstraction.DatabaseContexts;

namespace DataImporting.Abstraction.Services
{
	public interface ICachedOrganizationsIdsHelper
	{
		void AddId(string id);
		bool ContainsId(string id);
		void LoadData(IOrganizationsContext organizationsContext);
		void RemoveId(string id);
	}
}