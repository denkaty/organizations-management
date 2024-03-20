using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;

namespace Organizations.Business.Services
{
	public class IndustryService : IIndustryService
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IAPIResultFactory _apiResultFactory;
		private readonly IMapper _mapper;

		public IndustryService(IOrganizationsContext organizationsContext,
							  IMapper mapper,
							  IAPIResultFactory apiResultFactory)
		{
			_organizationsContext = organizationsContext;
			_mapper = mapper;
			_apiResultFactory = apiResultFactory;
		}
		public IAPIResult<ResultIndustryDTO> Create(CreateIndustryDTO createIndustryDTO)
		{
			Industry? existingIndustry = _organizationsContext.Industries.GetByName(createIndustryDTO.Name);

			if(existingIndustry != null) {
				if (existingIndustry.IsDeleted)
				{
					return _apiResultFactory.GetBadRequestResult<ResultIndustryDTO>(
						string.Format(Messages.ResourceIsSoftDeleted, "Industry", createIndustryDTO.Name));
				}

				return _apiResultFactory.GetBadRequestResult<ResultIndustryDTO>(
						string.Format(Messages.ResourceExists, "Industry", createIndustryDTO.Name));
			}

			Industry createdIndustry = _mapper.Map<Industry>(createIndustryDTO);
			_organizationsContext.Industries.Create(createdIndustry);

			ResultIndustryDTO resultIndustryDTO = _mapper.Map<ResultIndustryDTO>(createdIndustry);
			return _apiResultFactory.GetOKResult(resultIndustryDTO);
		}

		public IAPIResult<ResultIndustryDTO> GetById(string id)
		{
			Industry? existingIndustry = _organizationsContext.Industries.GetById(id);
			
			return existingIndustry == null
				   ? _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>(string.Format(Messages.ResourceNotFound, "Industry", id))
				   : _apiResultFactory.GetOKResult(_mapper.Map<ResultIndustryDTO>(existingIndustry));
		}

		public IAPIResult<ResultIndustryDTO> GetByName(string name)
		{
			Industry? existingIndustry = _organizationsContext.Industries.GetByName(name);

			return existingIndustry == null
				   ? _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>(string.Format(Messages.ResourceNotFound, "Industry", name))
				   : _apiResultFactory.GetOKResult(_mapper.Map<ResultIndustryDTO>(existingIndustry));
		}

		public IAPIResult<ICollection<ResultIndustryDTO>> GetAll()
		{
			ICollection<Industry> existingIndustries = _organizationsContext.Industries.GetAll();
			ICollection<ResultIndustryDTO> resultIndustryDTOs = _mapper.Map<ICollection<ResultIndustryDTO>>(existingIndustries);

			return _apiResultFactory.GetOKResult(resultIndustryDTOs);
		}
		public IAPIResult<ResultIndustryDTO> UpdateById(string id, UpdateIndustryDTO updateIndustryDTO)
		{
			Industry? existingIndustry = _organizationsContext.Industries.GetById(id);

			if (existingIndustry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>(string.Format(Messages.ResourceNotFound, "Industry", id));

			}

			if(existingIndustry.Name == updateIndustryDTO.Name)
			{
				return _apiResultFactory.GetBadRequestResult<ResultIndustryDTO>(string.Format(Messages.ResourceNameSameAsBefore, "Industry", existingIndustry.Name));
			}

			bool providedNameIsAlreadyUsed = _organizationsContext
										     .Industries
											 .GetAll(industry => industry.Name == updateIndustryDTO.Name)
											 .Any();
			if (providedNameIsAlreadyUsed)
			{
				return _apiResultFactory.GetBadRequestResult<ResultIndustryDTO>(string.Format(Messages.ResourceNameAlreadyExists, "Industry", updateIndustryDTO.Name));
			}

			existingIndustry = _mapper.Map(updateIndustryDTO, existingIndustry);
			_organizationsContext.Industries.UpdateById(id, existingIndustry);

			return _apiResultFactory.GetNoContentResult<ResultIndustryDTO>();
		}

		public IAPIResult<ResultIndustryDTO> SoftDeleteById(string id)
		{
			Industry? existingIndustry = _organizationsContext.Industries.GetById(id);

			if (existingIndustry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>(string.Format(Messages.ResourceNotFound, "Industry", id));
			}

			if (existingIndustry.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultIndustryDTO>(string.Format(Messages.ResourceIsSoftDeleted, "Industry", id));
			}

			_organizationsContext.Industries.SoftDeleteById(id);

			IEnumerable<OrganizationIndustry> organizationsIndustries = _organizationsContext.OrganizationsIndustries.GetAll(organization => organization.Industry_Id == id);
			foreach (var organizationIndustry in organizationsIndustries)
			{
				_organizationsContext.OrganizationsIndustries.DeleteByCompositeKey(organizationIndustry.Organization_Id, organizationIndustry.Industry_Id);
			}

			return _apiResultFactory.GetNoContentResult<ResultIndustryDTO>();
		}

		public IAPIResult<ResultIndustryDTO> RestoreById(string id)
		{
			var existingIndustry = _organizationsContext.Industries.GetById(id);

			if (existingIndustry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>(string.Format(Messages.ResourceNotFound, "Industry", id));
			}

			if (!existingIndustry.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultIndustryDTO>(string.Format(Messages.ResourceIsNotSoftDeleted, "Industry", id));
			}

			_organizationsContext.Industries.RestoreById(id);
			return _apiResultFactory.GetNoContentResult<ResultIndustryDTO>();
		}

	}
}
