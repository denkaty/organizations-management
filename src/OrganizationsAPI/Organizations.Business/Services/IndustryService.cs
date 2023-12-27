using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class IndustryService : IIndustryService
	{
		private readonly IOrganizationsDatabaseIndustryRepository _industryRepository;
		private readonly IAPIResultFactory _apiResultFactory;
		private readonly IMapper _mapper;

		public IndustryService(IOrganizationsDatabaseIndustryRepository industryRepository,
							  IMapper mapper,
							  IAPIResultFactory apiResultFactory)
		{
			_industryRepository = industryRepository;
			_mapper = mapper;
			_apiResultFactory = apiResultFactory;
		}
		public IAPIResult<ResultIndustryDTO> Create(CreateIndustryDTO createIndustryDTO)
		{
			Industry? existingIndustry = _industryRepository.GetByName(createIndustryDTO.Name);
			ResultIndustryDTO resultIndustryDTO;
			if (existingIndustry == null)
			{
				Industry createdIndustry = _mapper.Map<Industry>(createIndustryDTO);
				_industryRepository.Create(createdIndustry);

				resultIndustryDTO = _mapper.Map<ResultIndustryDTO>(createdIndustry);
				return _apiResultFactory.GetOKResult(resultIndustryDTO);
			}

			resultIndustryDTO = _mapper.Map<ResultIndustryDTO>(existingIndustry);
			return _apiResultFactory.GetOKResult(resultIndustryDTO);
		}

		public IAPIResult<ResultIndustryDTO> GetById(string id)
		{
			Industry? existingIndustry = _industryRepository.GetById(id);
			if (existingIndustry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>();
			}

			ResultIndustryDTO resultIndustryDTO = _mapper.Map<ResultIndustryDTO>(existingIndustry);
			return _apiResultFactory.GetOKResult(resultIndustryDTO);
		}

		public IAPIResult<ResultIndustryDTO> GetByName(string name)
		{
			Industry? existingIndustry = _industryRepository.GetByName(name);
			if (existingIndustry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>();
			}

			ResultIndustryDTO resultIndustryDTO = _mapper.Map<ResultIndustryDTO>(existingIndustry);
			return _apiResultFactory.GetOKResult(resultIndustryDTO);
		}

		public IAPIResult<ICollection<ResultIndustryDTO>> GetAll()
		{
			var existingIndustries = _industryRepository.GetAll();

			var resultIndustryDTOs = _mapper.Map<ICollection<ResultIndustryDTO>>(existingIndustries);

			return _apiResultFactory.GetOKResult(resultIndustryDTOs);
		}
		public IAPIResult<ResultIndustryDTO> UpdateById(string id, UpdateIndustryDTO updateIndustryDTO)
		{
			Industry? existingIndustry = _industryRepository.GetById(id);

			if (existingIndustry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>();
			}

			_mapper.Map(updateIndustryDTO, existingIndustry);

			_industryRepository.UpdateById(id, existingIndustry);

			var resultIndustryDTO = _mapper.Map<ResultIndustryDTO>(existingIndustry);

			return _apiResultFactory.GetOKResult(resultIndustryDTO);
		}

		public IAPIResult<ResultIndustryDTO> DeleteById(string id)
		{
			Industry? existingIndustry = _industryRepository.GetById(id);

			if (existingIndustry is null)
			{
				return _apiResultFactory.GetNotFoundResult<ResultIndustryDTO>();
			}

			_industryRepository.DeleteById(id);
			return _apiResultFactory.GetNoContentResult<ResultIndustryDTO>();
		}
		
	}
}
