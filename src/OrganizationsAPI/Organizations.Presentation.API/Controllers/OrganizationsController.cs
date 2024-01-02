using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Business.Models.Results.Base;
using Organizations.Business.Services;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Presentation.API.Extensions;

namespace Organizations.Presentation.API.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class OrganizationsController : ControllerBase
	{
		private readonly IOrganizationService _organizationsService;

		public OrganizationsController(IOrganizationService organizationsService)
		{
			_organizationsService = organizationsService;
		}

		[HttpPost]
		[Route("CreateOrganization")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult Create([FromBody] CreateOrganizationDTO createOrganizationDTO)
		{
			var apiResult = _organizationsService.Create(createOrganizationDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetOrganizationById/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetById([FromRoute] string id)
		{
			var apiResult = _organizationsService.GetById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetOrganizationByName/{name}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetByName([FromRoute] string name)
		{
			var apiResult = _organizationsService.GetByName(name);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetAllOrganizations")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetAll()
		{
			var apiResult = _organizationsService.GetAll();

			return this.HandleResponse(apiResult);

		}

		[HttpPut]
		[Route("UpdateOrganizationById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult Update([FromRoute] string id, [FromBody] UpdateOrganizationDTO updateOrganizationDTO)
		{
			var apiResult = _organizationsService.UpdateById(id, updateOrganizationDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpPatch]
		[Route("UpdateOrganizationCountry/{id}")]
		public IActionResult UpdateCountry([FromRoute] string id, PatchCountryDTO patchCountryDTO)
		{
			var apiResult = _organizationsService.UpdateCountry(id, patchCountryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpPost]
		[Route("AddIndustryToOrganization/{id}")]
		public IActionResult AddIndustry([FromRoute] string id, [FromBody] AddIndustryDTO addIndustryDTO)
		{
			var apiResult = _organizationsService.AddIndustry(id, addIndustryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpDelete]
		[Route("{id}/RemoveIndustryFromOrganization/{name}")]
		public IActionResult AddIndustry([FromRoute] string id, [FromRoute] string name)
		{
			var apiResult = _organizationsService.RemoveIndustry(id, name);

			return this.HandleResponse(apiResult);
		}

		[HttpDelete]
		[Route("SoftDeleteOrganizationById/{id}")]
		public IActionResult SoftDeleteById([FromRoute] string id)
		{
			var apiResult = _organizationsService.DeleteById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpPut]
		[Route("RestoreOrganizationById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult RestoreById([FromRoute] string id)
		{
			var apiResult = _organizationsService.RestoreById(id);

			return this.HandleResponse(apiResult);
		}

	}
}
