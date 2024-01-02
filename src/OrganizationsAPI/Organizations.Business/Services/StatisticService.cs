using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Statistic;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class StatisticService : IStatisticService
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IMapper _mapper;
		private readonly IAPIResultFactory _apiResultFactory;

		public StatisticService(IOrganizationsContext organizationsContext,
								IMapper mapper,
								IAPIResultFactory apiResultFactory)
		{
			_organizationsContext = organizationsContext;
			_mapper = mapper;
			_apiResultFactory = apiResultFactory;
		}

		public IAPIResult<ICollection<StatisticOrganizationDTO>> GetOrganizationsSortedByEmployeeCount()
		{
			ICollection<StatisticOrganization> organizations = _organizationsContext.Statistics.GetOrganizationsSortedByEmployeeCount();

			return _apiResultFactory.GetOKResult(_mapper.Map<ICollection<StatisticOrganizationDTO>>(organizations));
		}

		public IAPIResult<ICollection<StatisticEmployeesCountByIndustryDTO>> GetEmployeesCountByIndustry()
		{
			ICollection<StatisticEmployeesCountByIndustry> employeesCountByIndustries = _organizationsContext.Statistics.GetEmployeesCountByIndustry();

			return _apiResultFactory.GetOKResult(_mapper.Map<ICollection<StatisticEmployeesCountByIndustryDTO>>(employeesCountByIndustries));
		}

		public IAPIResult<ICollection<StatisticEmployeesCountByCountryAndIndustryDTO>> GetEmployeesCountByCountryAndIndustry()
		{
			ICollection<StatisticEmployeesCountByCountryAndIndustry> employeesCountByCountryAndIndustries = _organizationsContext.Statistics.GetEmployeesCountByCountryAndIndustry();

			return _apiResultFactory.GetOKResult(_mapper.Map<ICollection<StatisticEmployeesCountByCountryAndIndustryDTO>>(employeesCountByCountryAndIndustries));
		}
	}
}
