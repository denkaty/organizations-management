﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Business.Services;
using Organizations.Presentation.API.Extensions;
using Organizations.Presentation.API.Identity;

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

		[Authorize]
		[HttpPost]
		[Route("CreateIndustry")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Create([FromBody] CreateIndustryDTO createIndustryDTO)
		{
			var apiResult = _industryService.Create(createIndustryDTO);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetIndustryById/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetById([FromRoute] string id)
		{
			var apiResult = _industryService.GetById(id);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetIndustryByName/{name}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetByName([FromRoute] string name)
		{
			var apiResult = _industryService.GetByName(name);

			return this.HandleResponse(apiResult);
		}

		[HttpGet]
		[Route("GetAllIndustries")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetAll()
		{
			var apiResult = _industryService.GetAll();

			return this.HandleResponse(apiResult);

		}

		[Authorize]
		[HttpPut]
		[Route("UpdateIndustryById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateById([FromRoute] string id, [FromBody] UpdateIndustryDTO updateIndustryDTO)
		{
			var apiResult = _industryService.UpdateById(id, updateIndustryDTO);

			return this.HandleResponse(apiResult);
		}

		[Authorize(Policy = IdentityData.AdminUserPolicyName)]
		[HttpDelete]
		[Route("SoftDeleteIndustryById/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult DeleteById([FromRoute] string id)
		{
			var apiResult = _industryService.SoftDeleteById(id);

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
			var apiResult = _industryService.RestoreById(id);

			return this.HandleResponse(apiResult);
		}
	}
}
