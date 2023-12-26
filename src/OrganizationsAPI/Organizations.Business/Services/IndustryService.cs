using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.OrganizationsDatabase.Repositories;
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
			throw new NotImplementedException();
		}

		public IAPIResult<ResultIndustryDTO> DeleteById(string id)
		{
			throw new NotImplementedException();
		}

		public IAPIResult<ICollection<ResultIndustryDTO>> GetAll()
		{
			throw new NotImplementedException();
		}

		public IAPIResult<ResultIndustryDTO> GetById(string id)
		{
			throw new NotImplementedException();
		}

		public IAPIResult<ResultIndustryDTO> GetByName(string name)
		{
			throw new NotImplementedException();
		}

		public IAPIResult<ResultIndustryDTO> UpdateById(string id, UpdateIndustryDTO updateIndustryDTO)
		{
			throw new NotImplementedException();
		}
	}
}
