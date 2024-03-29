﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Models.Entities;

namespace Organizations.Presentation.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ExportsController : ControllerBase
	{
		private readonly IDataExportManager _exportService;

		public ExportsController(IDataExportManager exportService)
		{
			_exportService = exportService;
		}

		[Authorize]
		[HttpGet]
		[Route("Organizations/GeneratePDF/{id}")]
		public IActionResult GenerateOrganizationPDF([FromRoute]string id)
		{
			var resultOrganizationPDF = _exportService.ProcessGeneratingOrganizationPDF(id);
			if(resultOrganizationPDF == null)
			{
				return StatusCode(StatusCodes.Status404NotFound, string.Format(Messages.ResourceNotFound, "Organization", id));
			}

			return File(resultOrganizationPDF.PdfContent, "application/pdf", $"{resultOrganizationPDF.FileName}");
		}
	}
}
