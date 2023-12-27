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
		public IActionResult Create([FromBody] CreateOrganizationDTO createOrganizationDTO)
		{
			var apiResult = _organizationsService.Create(createOrganizationDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpGet("id/{id}")]
		public IActionResult GetById([FromRoute] string id)
		{
			var apiResult = _organizationsService.GetById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpGet("name/{name}")]
		public IActionResult GetByName([FromRoute] string name)
		{
			var apiResult = _organizationsService.GetByName(name);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var apiResult = _organizationsService.GetAll();

			return this.HandleResponse(apiResult);

		}


		[HttpPut("{id}")]
		public IActionResult Update([FromRoute] string id, [FromBody] UpdateOrganizationDTO updateOrganizationDTO)
		{
			var apiResult = _organizationsService.UpdateById(id, updateOrganizationDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			var apiResult = _organizationsService.DeleteById(id);

			return this.HandleResponse(apiResult);
		}

	}
}
