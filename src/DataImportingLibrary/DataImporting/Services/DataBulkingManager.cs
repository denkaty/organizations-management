using AutoMapper;
using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Abstraction.Factories;
using Organizations.Data.Models.Entities;


namespace DataImporting.Services
{
	public class DataBulkingManager : IDataBulkingManager
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly ICachedOrganizationsIdsHelper _cachedOrganizationsIdsHelper;
		private readonly IMapper _mapper;
		private readonly IEntityFactory _entityFactory;

		private Dictionary<string, string> _availableIndustries;
		private Dictionary<string, string> _availableCountries;
		public DataBulkingManager(IOrganizationsContext organizationsContext,
								  ICachedOrganizationsIdsHelper cachedOrganizationsIdsHelper,
								  IMapper mapper,
								  IEntityFactory entityFactory)
		{
			_organizationsContext = organizationsContext;
			_cachedOrganizationsIdsHelper = cachedOrganizationsIdsHelper;
			_mapper = mapper;
			_entityFactory = entityFactory;

			_availableCountries = new Dictionary<string, string>();
			_availableIndustries = new Dictionary<string, string>();
		}

		public BulkedDataWrapper BulkData(ICollection<NormalizedOrganization> importOrganizations)
		{
			LoadCurrentData();

			var bulkCollection = new BulkedDataWrapper();
			foreach (var importOrganization in importOrganizations)
			{
				ProcessOrganizationBulking(importOrganization, bulkCollection);
			}

			ClearData();

			return bulkCollection;
		}

		private void ProcessOrganizationBulking(NormalizedOrganization organizationData, BulkedDataWrapper bulkedDataWrapper)
		{
			if (_cachedOrganizationsIdsHelper.ContainsId(organizationData.OrganizationId))
			{
				return;
			}

			_cachedOrganizationsIdsHelper.AddId(organizationData.OrganizationId);

			var organizationBulk = _mapper.Map<Organization>(organizationData);

			organizationBulk.CountryId = ProcessCountryBulking(organizationData.Country, bulkedDataWrapper.BulkedCountries);

			var industryIds = ProcessIndustriesBulking(organizationData.Industries, bulkedDataWrapper.BulkedIndustries);

			ProcessOrganizationsIndustriesBulking(organizationData.OrganizationId, bulkedDataWrapper, industryIds);

			bulkedDataWrapper.BulkedOrganizations.Add(organizationBulk);
		}

		private string ProcessCountryBulking(string countryName, ICollection<Country> bulkedCountries)
		{
			if (!_availableCountries.TryGetValue(countryName, out var countryId))
			{
				var country = _entityFactory.CreateCountry(countryName);
				bulkedCountries.Add(country);
				_availableCountries.Add(country.Name, country.Id);
				countryId = country.Id;
			}

			return countryId;
		}

		private ICollection<string> ProcessIndustriesBulking(IEnumerable<string> industryNames, ICollection<Industry> bulkedIndustries)
		{
			var industryIds = new List<string>();

			foreach (var industryName in industryNames)
			{
				if (!_availableIndustries.TryGetValue(industryName, out var industryId))
				{
					var industry = _entityFactory.CreateIndustry(industryName);
					bulkedIndustries.Add(industry);
					_availableIndustries.Add(industry.Name, industry.Id);
					industryId = industry.Id;
				}

				industryIds.Add(industryId);
			}

			return industryIds;
		}
		private void ProcessOrganizationsIndustriesBulking(string organizationId, BulkedDataWrapper bulkedDataWrapper, ICollection<string> industryIds)
		{
			foreach (var industryId in industryIds)
			{
				var organizationIndustry = _entityFactory.CreateOrganizationIndustry(organizationId, industryId);
				bulkedDataWrapper.BulkedOrganizationsIndustries.Add(organizationIndustry);
			}
		}

		private void LoadCurrentData()
		{
			_availableCountries = _organizationsContext.Countries
				.GetAll()
				.ToDictionary(c => c.Name, c => c.Id);

			_availableIndustries = _organizationsContext.Industries
				.GetAll()
				.ToDictionary(c => c.Name, c => c.Id);
		}

		private void ClearData()
		{
			_availableCountries.Clear();
			_availableIndustries.Clear();
		}
	}
}
