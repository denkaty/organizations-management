using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Presentation.API.Extensions;

namespace Organizations.Presentation.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class IndustriesController : ControllerBase
	{
		private readonly IIndustryService _industryService;
		public IndustriesController(IIndustryService industryService)
		{
			_industryService = industryService;
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateIndustryDTO createIndustryDTO)
		{
			var apiResult = _industryService.Create(createIndustryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpGet("id/{id}")]
		public IActionResult GetById([FromRoute] string id)
		{
			var apiResult = _industryService.GetById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpGet("name/{name}")]
		public IActionResult GetByName([FromRoute] string name)
		{
			var apiResult = _industryService.GetByName(name);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var apiResult = _industryService.GetAll();

			return this.HandleResponse(apiResult);

		}

		[HttpPut("{id}")]
		public IActionResult UpdateById([FromRoute] string id, [FromBody] UpdateIndustryDTO updateIndustryDTO)
		{
			var apiResult = _industryService.UpdateById(id, updateIndustryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteById([FromRoute] string id)
		{
			var apiResult = _industryService.DeleteById(id);

			return this.HandleResponse(apiResult);
		}
	}
}
