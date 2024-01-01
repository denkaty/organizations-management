using AutoMapper;
using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class DataImporter : IDataImporter
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IMapper _mapper;

		public DataImporter(IOrganizationsContext organizationsContext, 
							IMapper mapper)
		{
			_organizationsContext = organizationsContext;
			_mapper = mapper;
		}

		public void ImportData(ICollection<NormalizedOrganization> organizations)
		{
			foreach (var currentOrganization in organizations)
			{
				Organization? existingOrganization = _organizationsContext.Organizations.GetByName(currentOrganization.Name);
				if (existingOrganization != null)
				{
					continue;
				}

				Organization newOrganization = _mapper.Map<Organization>(currentOrganization);


				Country? existingCountry = _organizationsContext.Countries.GetByName(currentOrganization.Country);
				if (existingCountry == null)
				{
					Country newCountry = _mapper.Map<Country>(currentOrganization); //does it use the Country - : id
					_organizationsContext.Countries.Create(newCountry);
					newOrganization.CountryId = newCountry.Id;
				}
				else
				{
					newOrganization.CountryId = existingCountry.Id;
				}
				_organizationsContext.Organizations.Create(newOrganization);


				foreach (var industryName in currentOrganization.Industries)
				{
					Industry? existingIndustry = _organizationsContext.Industries.GetByName(industryName);
					if(existingIndustry == null)
					{
						Industry newIndustry = new Industry();
						newIndustry.Name = industryName;
						_organizationsContext.Industries.Create(newIndustry);

						OrganizationIndustry organizationIndustry = new OrganizationIndustry();
						organizationIndustry.Organization_Id = newOrganization.Id;
						organizationIndustry.Industry_Id = newIndustry.Id;
						_organizationsContext.OrganizationsIndustries.Create(organizationIndustry);
					}
					else
					{
						OrganizationIndustry organizationIndustry = new OrganizationIndustry();
						organizationIndustry.Organization_Id = newOrganization.Id;
						organizationIndustry.Industry_Id = existingIndustry.Id;
						_organizationsContext.OrganizationsIndustries.Create(organizationIndustry);
					}
				}
				

			}
		}
	}
}
