using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Presentation.API.Extensions;
using Organizations.Presentation.API.Identity;

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

		[Authorize]
		[HttpPost]
		[Route("CreateCountry")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Create([FromBody] CreateCountryDTO createCountryDTO)
		{
			var apiResult = _countryService.Create(createCountryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetCountryById/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetById([FromRoute] string id)
		{
			var apiResult = _countryService.GetById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetCountryByName/{name}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetByName([FromRoute] string name)
		{
			var apiResult = _countryService.GetByName(name);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetAllCountries")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetAll()
		{
			var apiResult = _countryService.GetAll();

			return this.HandleResponse(apiResult);

		}

		[Authorize]
		[HttpPut]
		[Route("UpdateCountryById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateById([FromRoute] string id, [FromBody] UpdateCountryDTO updateCountryDTO)
		{
			var apiResult = _countryService.UpdateById(id, updateCountryDTO);

			return this.HandleResponse(apiResult);
		}

		[Authorize(Policy = IdentityData.AdminUserPolicyName)]
		[HttpDelete]
		[Route("SoftDeleteCountryById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult SoftDeleteById([FromRoute] string id)
		{
			var apiResult = _countryService.SoftDeleteById(id);

			return this.HandleResponse(apiResult);
		}

		[Authorize(Policy = IdentityData.AdminUserPolicyName)]
		[HttpPut]
		[Route("RestoreCountryById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult RestoreById([FromRoute] string id)
		{
			var apiResult = _countryService.RestoreById(id);

			return this.HandleResponse(apiResult);
		}

	}
}
