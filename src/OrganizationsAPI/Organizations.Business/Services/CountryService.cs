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
	public class CountryService : ICountryService
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IAPIResultFactory _apiResultFactory;
		private readonly IMapper _mapper;

		public CountryService(IOrganizationsContext organizationsContext,
							  IMapper mapper,
							  IAPIResultFactory apiResultFactory)
		{
			_organizationsContext = organizationsContext;
			_mapper = mapper;
			_apiResultFactory = apiResultFactory;
		}

		public IAPIResult<ResultCountryDTO> Create(CreateCountryDTO createCountryDTO)
		{
			Country? existingCountry = _organizationsContext.Countries.GetByName(createCountryDTO.Name);

			if (existingCountry != null)
			{
				if (existingCountry.IsDeleted)
				{
					return _apiResultFactory.GetBadRequestResult<ResultCountryDTO>(
						string.Format(Messages.ResourceIsSoftDeleted, "Country", createCountryDTO.Name));
				}

				return _apiResultFactory.GetBadRequestResult<ResultCountryDTO>(
					string.Format(Messages.ResourceExists, "Country", createCountryDTO.Name));
			}

			Country createdCountry = _mapper.Map<Country>(createCountryDTO);
			_organizationsContext.Countries.Create(createdCountry);

			ResultCountryDTO resultCountryDTO = _mapper.Map<ResultCountryDTO>(createdCountry);
			return _apiResultFactory.GetOKResult(resultCountryDTO);
		}

		public IAPIResult<ResultCountryDTO> GetById(string id)
		{
			Country? existingCountry = _organizationsContext.Countries.GetById(id);

			return existingCountry == null
				   ? _apiResultFactory.GetNotFoundResult<ResultCountryDTO>(string.Format(Messages.ResourceNotFound, "Country", id))
				   : _apiResultFactory.GetOKResult(_mapper.Map<ResultCountryDTO>(existingCountry));
		}

		public IAPIResult<ResultCountryDTO> GetByName(string name)
		{
			Country? existingCountry = _organizationsContext.Countries.GetByName(name);

			return existingCountry == null
				? _apiResultFactory.GetNotFoundResult<ResultCountryDTO>(string.Format(Messages.ResourceNotFound, "Country", name))
				: _apiResultFactory.GetOKResult(_mapper.Map<ResultCountryDTO>(existingCountry));
		}

		public IAPIResult<ICollection<ResultCountryDTO>> GetAll()
		{
			ICollection<Country> existingCountries = _organizationsContext.Countries.GetAll();
			ICollection<ResultCountryDTO> resultCountryDTOs = _mapper.Map<ICollection<ResultCountryDTO>>(existingCountries);

			return _apiResultFactory.GetOKResult(resultCountryDTOs);
		}

		public IAPIResult<ResultCountryDTO> UpdateById(string id, UpdateCountryDTO updateCountryDTO)
		{
			Country? existingCountry = _organizationsContext.Countries.GetById(id);

			if (existingCountry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>(string.Format(Messages.ResourceNotFound, "Country", id));
			}

			if (existingCountry.Name == updateCountryDTO.Name)
			{
				return _apiResultFactory.GetBadRequestResult<ResultCountryDTO>(string.Format(Messages.ResourceNameSameAsBefore, "Country", existingCountry.Name));
			}

			bool providedNameIsAlreadyUsed = _organizationsContext
									         .Countries
											 .GetAll(country => country.Name == updateCountryDTO.Name)
											 .Any();
			if (providedNameIsAlreadyUsed)
			{
				return _apiResultFactory.GetBadRequestResult<ResultCountryDTO>(string.Format(Messages.ResourceNameAlreadyExists, "Country", updateCountryDTO.Name));
			}

			existingCountry = _mapper.Map(updateCountryDTO, existingCountry);
			_organizationsContext.Countries.UpdateById(id, existingCountry);

			return _apiResultFactory.GetNoContentResult<ResultCountryDTO>();
		}

		public IAPIResult<ResultCountryDTO> SoftDeleteById(string id)
		{
			var existingCountry = _organizationsContext.Countries.GetById(id);

			if (existingCountry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>(string.Format(Messages.ResourceNotFound, "Country", id));
			}

			if (existingCountry.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultCountryDTO>(string.Format(Messages.ResourceIsSoftDeleted, "Country", id));
			}

			_organizationsContext.Countries.SoftDeleteById(id);

			IEnumerable<Organization> organizations = _organizationsContext.Organizations.GetAll(organization => organization.CountryId == id);
			foreach (var organization in organizations)
			{
				_organizationsContext.Organizations.UpdateCountryToNull(organization.Id);
			}

			return _apiResultFactory.GetNoContentResult<ResultCountryDTO>();
		}

		public IAPIResult<ResultCountryDTO> RestoreById(string id)
		{
			var existingCountry = _organizationsContext.Countries.GetById(id);

			if (existingCountry == null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>(string.Format(Messages.ResourceNotFound, "Country", id));
			}

			if (!existingCountry.IsDeleted)
			{
				return _apiResultFactory.GetBadRequestResult<ResultCountryDTO>(string.Format(Messages.ResourceIsNotSoftDeleted, "Country", id));
			}

			_organizationsContext.Countries.RestoreById(id);
			return _apiResultFactory.GetNoContentResult<ResultCountryDTO>();
		}
	}
}
