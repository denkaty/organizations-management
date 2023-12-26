using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Organizations.Business.Services
{
	public class CountryService : ICountryService
	{
		private readonly IOrganizationsDatabaseCountryRepository _countryRepository;
		private readonly IAPIResultFactory _apiResultFactory;
		private readonly IMapper _mapper;

		public CountryService(IOrganizationsDatabaseCountryRepository countryRepository,
							  IMapper mapper,
							  IAPIResultFactory apiResultFactory)
		{
			_countryRepository = countryRepository;
			_mapper = mapper;
			_apiResultFactory = apiResultFactory;
		}
		public IAPIResult<ResultCountryDTO> Create(CreateCountryDTO createCountryDTO)
		{
			Country? existingCountry = _countryRepository.GetByName(createCountryDTO.Name);
			ResultCountryDTO resultCountryDTO;
			if (existingCountry == null)
			{
				Country createdCountry = _mapper.Map<Country>(createCountryDTO);
				_countryRepository.Create(createdCountry);

				resultCountryDTO = _mapper.Map<ResultCountryDTO>(createdCountry);
				return _apiResultFactory.GetOKResult(resultCountryDTO);
			}

			resultCountryDTO = _mapper.Map<ResultCountryDTO>(existingCountry);
			return _apiResultFactory.GetOKResult(resultCountryDTO);
		}
		public IAPIResult<ResultCountryDTO> GetById(string id)
		{
			Country? existingCountry = _countryRepository.GetById(id);
			if (existingCountry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>();
			}

			ResultCountryDTO resultCountryDTO = _mapper.Map<ResultCountryDTO>(existingCountry);
			return _apiResultFactory.GetOKResult(resultCountryDTO);
		}
		public IAPIResult<ResultCountryDTO> GetByName(string name)
		{
			Country? existingCountry = _countryRepository.GetByName(name);
			if (existingCountry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>();
			}

			ResultCountryDTO resultCountryDTO = _mapper.Map<ResultCountryDTO>(existingCountry);
			return _apiResultFactory.GetOKResult(resultCountryDTO);
		}
		public IAPIResult<ICollection<ResultCountryDTO>> GetAll()
		{
			var existingCountries = _countryRepository.GetAll();

			var resultCountryDTOs = _mapper.Map<ICollection<ResultCountryDTO>>(existingCountries);

			return _apiResultFactory.GetOKResult(resultCountryDTOs);
		}
		public IAPIResult<ResultCountryDTO> UpdateById(string id, UpdateCountryDTO updateCountryDTO)
		{
			Country? existingCountry = _countryRepository.GetById(id);

			if (existingCountry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>();
			}

			_mapper.Map(updateCountryDTO, existingCountry);

			_countryRepository.UpdateById(id, existingCountry);

			return _apiResultFactory.GetNoContentResult<ResultCountryDTO>();
		}

		public IAPIResult<ResultCountryDTO> DeleteById(string id)
		{
			var existingCountry = _countryRepository.GetById(id);

			if (existingCountry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultCountryDTO>();
			}

			_countryRepository.DeleteById(id);
			return _apiResultFactory.GetNoContentResult<ResultCountryDTO>();
		}

	}
}
