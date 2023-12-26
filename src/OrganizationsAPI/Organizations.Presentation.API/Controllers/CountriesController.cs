using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Presentation.API.Extensions;

namespace Organizations.Presentation.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		private readonly ICountryService _countryService;
		public CountriesController(ICountryService countryService)
		{
			_countryService = countryService;
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateCountryDTO createCountryDTO)
		{
			var apiResult = _countryService.Create(createCountryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpGet("id/{id}")]
		public IActionResult GetById([FromRoute] string id)
		{
			var apiResult = _countryService.GetById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpGet("name/{name}")]
		public IActionResult GetByName([FromRoute] string name)
		{
			var apiResult = _countryService.GetByName(name);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var apiResult = _countryService.GetAll();

			return this.HandleResponse(apiResult);

		}

		[HttpPut("{id}")]
		public IActionResult UpdateById([FromRoute] string id, [FromBody] UpdateCountryDTO updateCountryDTO)
		{
			var apiResult = _countryService.UpdateById(id, updateCountryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteById([FromRoute] string id)
		{
			var apiResult = _countryService.DeleteById(id);

			return this.HandleResponse(apiResult);
		}

	}
}
