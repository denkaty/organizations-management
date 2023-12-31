using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
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
			if (existingOrganization != null)
			{
				if (existingOrganization.IsDeleted)
				{
					return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(
						string.Format(Messages.ResourceIsSoftDeleted, "Organization", createOrganizationDTO.Name));
				}

				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(
					string.Format(Messages.ResourceExists, "Organization", createOrganizationDTO.Name));
			}

			Country? existingCountry = _organizationsContext.Countries.GetByName(createOrganizationDTO.Country);
			if (existingCountry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceDoesNotExist, "Country", createOrganizationDTO.Country));
			}

			existingOrganization = _mapper.Map<Organization>(createOrganizationDTO);
			existingOrganization.CountryId = existingCountry.Id;
			_organizationsContext.Organizations.Create(existingOrganization);

			var industryIds = new List<string>();
			foreach (string industryName in createOrganizationDTO.Industries)
			{
				Industry? existingIndustry = _organizationsContext.Industries.GetByName(industryName);
				if (existingIndustry == null)
				{
					return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.ResourceDoesNotExist, "Industry", industryName));
				}
				OrganizationIndustry? existingOrganizationIndustry = _organizationsContext.OrganizationsIndustries.GetByCompositeKey(existingOrganization.Id, existingIndustry.Id);
				if(existingOrganizationIndustry != null)
				{
					return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.JunctionResourceExists, "OrganizationIndustry", existingOrganizationIndustry.Organization_Id, existingOrganizationIndustry.Industry_Id));
				}
				OrganizationIndustry organizationIndustry = new OrganizationIndustry
				{
					Organization_Id = existingOrganization.Id,
					Industry_Id = existingIndustry.Id
				};
				_organizationsContext.OrganizationsIndustries.Create(organizationIndustry);
				industryIds.Add(organizationIndustry.Industry_Id);
			}

			ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
			resultOrganizationDTO.Industries = industryIds;
			return _apiResultFactory.GetOKResult(resultOrganizationDTO);
		}

		public IAPIResult<ResultOrganizationDTO> GetById(string id)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(id);

			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", id));
			}

			ICollection<string> industryIds = FetchIndustryIdsByOrganizationId(existingOrganization.Id);
			ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
			resultOrganizationDTO.Industries = industryIds;

			return _apiResultFactory.GetOKResult(resultOrganizationDTO);
		}

		public IAPIResult<ResultOrganizationDTO> GetByName(string name)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetByName(name);

			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", name));
			}

			ICollection<string> industryIds = FetchIndustryIdsByOrganizationId(existingOrganization.Id);
			ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
			resultOrganizationDTO.Industries = industryIds;

			return _apiResultFactory.GetOKResult(resultOrganizationDTO);
		}

		public IAPIResult<ICollection<ResultOrganizationDTO>> GetAll()
		{
			ICollection<Organization> existingOrganizations = _organizationsContext.Organizations.GetAll();
			ICollection<ResultOrganizationDTO> resultOrganizationDTOs = new List<ResultOrganizationDTO>();
			foreach (var existingOrganization in existingOrganizations)
			{
				Country existingCountry = _organizationsContext.Countries.GetByName(existingOrganization.CountryId)!;

				ICollection<string> industryIds = FetchIndustryIdsByOrganizationId(existingOrganization.Id);

				ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
				resultOrganizationDTO.Industries = industryIds;

				resultOrganizationDTOs.Add(resultOrganizationDTO);
			}

			return _apiResultFactory.GetOKResult(resultOrganizationDTOs);
		}

		public IAPIResult<ResultOrganizationDTO> UpdateById(string id, UpdateOrganizationDTO updateOrganizationDTO)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(id);
			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(
					string.Format(Messages.ResourceNotFound, "Organization", updateOrganizationDTO.Name));
			}

			if (existingOrganization.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(
					string.Format(Messages.ResourceIsSoftDeleted, "Organization", updateOrganizationDTO.Name));
			}

			Country? existingCountry = _organizationsContext.Countries.GetByName(updateOrganizationDTO.Country);
			if (existingCountry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceDoesNotExist, "Country", updateOrganizationDTO.Country));
			}

			List<string> industryIds = new List<string>();
			foreach (var industryName in updateOrganizationDTO.Industries)
			{
				Industry? existingIndustry = _organizationsContext.Industries.GetByName(industryName);
				if (existingIndustry == null)
				{
					return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceDoesNotExist, "Industry", industryName));
				}

				OrganizationIndustry? existingOrganizationIndustry = _organizationsContext.OrganizationsIndustries.GetByCompositeKey(id, existingIndustry.Id);
				if(existingOrganizationIndustry != null)
				{
					continue;
				}

				OrganizationIndustry organizationIndustry = new OrganizationIndustry
				{
					Organization_Id = existingOrganization.Id,
					Industry_Id = existingIndustry.Id
				};
				_organizationsContext.OrganizationsIndustries.Create(organizationIndustry);
				industryIds.Add(organizationIndustry.Industry_Id);
			}

			existingOrganization = _mapper.Map<Organization>(updateOrganizationDTO);
			existingOrganization.CountryId = existingCountry.Id;
			existingOrganization.Id = id;
			_organizationsContext.Organizations.UpdateById(id, existingOrganization);

			return _apiResultFactory.GetNoContentResult<ResultOrganizationDTO>();
		}

		public IAPIResult<ResultOrganizationDTO> UpdateCountry(string organizationId, PatchCountryDTO patchCountryDTO)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(organizationId);

			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", organizationId));
			}

			if(patchCountryDTO.CountryName == null)
			{
				existingOrganization.OrganizationId = null;
				_organizationsContext.Organizations.UpdateCountryToNull(organizationId);
			}

			Country? existingCountry = _organizationsContext.Countries.GetByName(patchCountryDTO.CountryName);
			if(existingCountry == null)
			{
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.ResourceDoesNotExist, "Country", patchCountryDTO.CountryName));
			}

			if (existingOrganization.CountryId == existingCountry.Id) {
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNameSameAsBefore, "Country", existingCountry.Name));
			}

			existingOrganization.CountryId = existingCountry.Id;
			_organizationsContext.Organizations.UpdateCountry(organizationId, existingCountry.Id);

			ResultOrganizationDTO resultOrganizationDTO = _mapper.Map<ResultOrganizationDTO>(existingOrganization);
			resultOrganizationDTO.Industries = FetchIndustryIdsByOrganizationId(organizationId);
			return _apiResultFactory.GetOKResult(resultOrganizationDTO);
		}

		public IAPIResult<ResultOrganizationDTO> DeleteById(string id)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(id);

			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", id));
			}

			if (existingOrganization.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.ResourceIsSoftDeleted, "Organization", id));
			}

			_organizationsContext.Organizations.SoftDeleteById(id);
			return _apiResultFactory.GetNoContentResult<ResultOrganizationDTO>();
		}
		public IAPIResult<ResultOrganizationDTO> RestoreById(string id)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(id);

			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", id));
			}

			if (!existingOrganization.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.ResourceIsNotSoftDeleted, "Organization", id));
			}

			_organizationsContext.Organizations.RestoreById(id);
			return _apiResultFactory.GetNoContentResult<ResultOrganizationDTO>();
		}

		private ICollection<string> FetchIndustryIdsByOrganizationId(string organizationId)
		{
			return _organizationsContext.OrganizationsIndustries
					   .GetByFirstKey(organizationId)
					   .Select(organization => organization.Industry_Id)
					   .ToList();
		}

		public IAPIResult<ResultOrganizationDTO> AddIndustry(string organizationId, AddIndustryDTO addIndustryDTO)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(organizationId);
			if(existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", organizationId));
			}

			Industry? existingIndustry = _organizationsContext.Industries.GetByName(addIndustryDTO.Name);
			if(existingIndustry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Industry", addIndustryDTO.Name));
			}

			OrganizationIndustry existingOrganizationIndustry = _organizationsContext.OrganizationsIndustries.GetByCompositeKey(organizationId, existingIndustry.Id);
			if( existingOrganizationIndustry != null) {
				_apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.JunctionResourceExists,"OrganizationIndustry", organizationId, existingIndustry.Id));
			}

			OrganizationIndustry organizationIndustry = new OrganizationIndustry
			{
				Organization_Id = organizationId,
				Industry_Id = existingIndustry.Id
			};

			_organizationsContext.OrganizationsIndustries.Create(organizationIndustry);
			return _apiResultFactory.GetNoContentResult<ResultOrganizationDTO>();
		}

		public IAPIResult<ResultOrganizationDTO> RemoveIndustry(string id, string name)
		{
			Organization? existingOrganization = _organizationsContext.Organizations.GetById(id);
			if (existingOrganization == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Organization", id));
			}

			Industry? existingIndustry = _organizationsContext.Industries.GetByName(name);
			if (existingIndustry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultOrganizationDTO>(string.Format(Messages.ResourceNotFound, "Industry", name));
			}

			OrganizationIndustry? existingOrganizationIndustry = _organizationsContext.OrganizationsIndustries.GetByCompositeKey(id, existingIndustry.Id);
			if (existingOrganizationIndustry == null)
			{
				return _apiResultFactory.GetBadRequestResult<ResultOrganizationDTO>(string.Format(Messages.JunctionResourceDoesNotExists, "OrganizationIndustry", id, existingIndustry.Id));
			}

			_organizationsContext.OrganizationsIndustries.DeleteByCompositeKey(id, existingIndustry.Id);
			return _apiResultFactory.GetNoContentResult<ResultOrganizationDTO>();
		}
	}
}
