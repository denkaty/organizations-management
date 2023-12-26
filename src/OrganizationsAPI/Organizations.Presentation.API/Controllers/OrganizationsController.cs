using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Data.Abstraction.DatabaseContexts;

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

		public OrganizationsController()
		{
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateOrganizationDTO createOrganizationDTO)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			throw new NotImplementedException();

		}

		[HttpGet("{id}")]
		public IActionResult GetById([FromRoute] string id)
		{
			throw new NotImplementedException();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromRoute] string id, [FromBody] UpdateOrganizationDTO updateOrganizationDTO)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] string id)
		{
			throw new NotImplementedException();
		}

	}
}
