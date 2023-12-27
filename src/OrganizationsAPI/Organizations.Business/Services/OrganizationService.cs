using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Organizations.Business.Services
{
	public class OrganizationService : IOrganizationService
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IAPIResultFactory _apiResultFactory;
		private readonly IMapper _mapper;
		public OrganizationService(IOrganizationsContext organizationsContext,
								   IAPIResultFactory apiResultFactory,
								   IMapper mapper)
		{
			_organizationsContext = organizationsContext;
			_mapper = mapper;
			_apiResultFactory = apiResultFactory;
		}

		public IAPIResult<ResultOrganizationDTO> Create(CreateOrganizationDTO createOrganizationDTO)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetByName(createOrganizationDTO.Name);
			var industryIds = new List<string>();
			if (existingOrganization == null)
			{
				Country? existingCountry = _organizationsContext.Countries.GetByName(createOrganizationDTO.Country);
				if (existingCountry == null)
				{
					return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>([string.Format(Messages.CountryDoesNotExist, createOrganizationDTO.Country)]);
				}
				else
				{
					existingOrganization = _mapper.Map<Organization>(createOrganizationDTO);
					existingOrganization.CountryId = existingCountry.Id;
				}
				_organizationsContext.Organizations.Create(existingOrganization);
				foreach (string industryName in createOrganizationDTO.Industries)
				{
					Industry? existingIndustry = _organizationsContext.Industries.GetByName(industryName);
					if (existingIndustry == null)
					{
						return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>([string.Format(Messages.CountryDoesNotExist, industryName)]);
					}
					else
					{
						OrganizationIndustry organizationIndustry = new OrganizationIndustry
						{
							Organization_Id = existingOrganization.Id,
							Industry_Id = existingIndustry.Id
						};
						industryIds.Add(organizationIndustry.Industry_Id);
						_organizationsContext.OrganizationsIndustries.Create(organizationIndustry);
					}
				}

				ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
				resultOrganizationDTO.Industries = industryIds;
				return _apiResultFactory.GetOKResult(resultOrganizationDTO);
			}
			else
			{
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>([string.Format(Messages.OrganizationExists, createOrganizationDTO.Name)]);
			}

		}

		public IAPIResult<ResultOrganizationDTO> GetById(string id)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(id);

			if (existingOrganization != null)
			{
				Country existingCountry = _organizationsContext.Countries.GetById(existingOrganization.CountryId);

				ICollection<string> industryIds = _organizationsContext.OrganizationsIndustries
																.GetByFirstKey(existingOrganization.Id)
																.Select(organization => organization.Industry_Id)
																.ToList();

				ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
				resultOrganizationDTO.Industries = industryIds;

				return _apiResultFactory.GetOKResult(resultOrganizationDTO);
			}
			else
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>([string.Format(Messages.OrganizationNotFound, id)]);
			}
		}

		public IAPIResult<ResultOrganizationDTO> GetByName(string name)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetByName(name);

			if (existingOrganization != null)
			{
				Country existingCountry = _organizationsContext.Countries.GetByName(existingOrganization.CountryId)!;

				ICollection<string> industryIds = _organizationsContext.OrganizationsIndustries
																.GetByFirstKey(existingOrganization.Id)
																.Select(organization => organization.Industry_Id)
																.ToList();

				ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
				resultOrganizationDTO.Industries = industryIds;

				return _apiResultFactory.GetOKResult(resultOrganizationDTO);
			}
			else
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>([string.Format(Messages.OrganizationNotFound, name)]);
			}
		}

		public IAPIResult<ICollection<ResultOrganizationDTO>> GetAll()
		{
			ICollection<Organization> existingOrganizations = _organizationsContext.Organizations.GetAll();
			ICollection<ResultOrganizationDTO> resultOrganizationDTOs = new List<ResultOrganizationDTO>();
			foreach (var existingOrganization in existingOrganizations)
			{
				Country existingCountry = _organizationsContext.Countries.GetByName(existingOrganization.CountryId)!;

				ICollection<string> industryIds = _organizationsContext.OrganizationsIndustries
																.GetByFirstKey(existingOrganization.Id)
																.Select(organization => organization.Industry_Id)
																.ToList();

				ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
				resultOrganizationDTO.Industries = industryIds;

				resultOrganizationDTOs.Add(resultOrganizationDTO);
			}

			return _apiResultFactory.GetOKResult(resultOrganizationDTOs);
		}

		public IAPIResult<ResultOrganizationDTO> UpdateById(string id, UpdateOrganizationDTO updateOrganizationDTO)
		{
			throw new NotImplementedException();
		}

		public IAPIResult<ResultOrganizationDTO> DeleteById(string id)
		{
			throw new NotImplementedException();
		}

	}
}
