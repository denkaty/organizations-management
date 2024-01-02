using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Presentation.API.Extensions;

namespace Organizations.Presentation.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StatisticsController : ControllerBase
	{
		private readonly IStatisticService _statisticService;

		public StatisticsController(IStatisticService statisticService)
		{
			_statisticService = statisticService;
		}

		[HttpGet]
		[Route("Organizations/SortedByEmployeeCount")]
		public IActionResult GetOrganizationsSortedByEmployeeCount()
		{
			var apiResult = _statisticService.GetOrganizationsSortedByEmployeeCount();

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("TotalEmployeesByIndustry")]
		public IActionResult GetTotalEmployeesByIndustry()
		{
			var apiResult = _statisticService.GetEmployeesCountByIndustry();

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("EmployeesByCountryIndustry")]
		public IActionResult GetEmployeesByCountryAndIndustry()
		{
			var apiResult = _statisticService.GetEmployeesCountByCountryAndIndustry();

			return this.HandleResponse(apiResult);
		}

	}
}
