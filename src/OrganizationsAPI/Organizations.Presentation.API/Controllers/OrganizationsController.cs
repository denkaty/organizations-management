using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Models.DTOs;

namespace Organizations.Presentation.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrganizationsController : ControllerBase
	{
		[HttpPost]
		public IActionResult Create([FromBody] CreateOrganizationDTO createOrganizationDTO)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		public IActionResult ReadAll()
		{
			throw new NotImplementedException();
		}

		[HttpGet("{id}")]
		public IActionResult Read([FromRoute] string id)
		{
			throw new NotImplementedException();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromBody] UpdateOrganizationDTO updateOrganizationDTO, [FromRoute]string id)
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
