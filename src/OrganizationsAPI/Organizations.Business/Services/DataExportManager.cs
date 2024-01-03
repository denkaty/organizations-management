using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Export;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class DataExportManager : IDataExportManager
	{
		private readonly IOrganizationService _organizationService;
		private readonly IOrganizationHTMLGenerator _organizationHTMLGenerator;
		private readonly IPDFGenerator _pdfGenerator;

		public DataExportManager(IOrganizationService organizationService,
								 IOrganizationHTMLGenerator organizationHTMLGenerator,
								 IPDFGenerator pdfGenerator)
		{
			_organizationService = organizationService;
			_organizationHTMLGenerator = organizationHTMLGenerator;
			_pdfGenerator = pdfGenerator;
		}


		public ResultOrganizationPDF? ProcessGeneratingOrganizationPDF(string id)
		{
			IAPIResult<ResultOrganizationDTO> iAPIResult = _organizationService.GetByIdReplacingForeignKeys(id);
			int statusCode = (int)iAPIResult.StatusCode;
			if(statusCode < 200 || statusCode >= 300)
			{
				return null;
			}

			ResultOrganizationDTO organization = iAPIResult.Data;
			var htmlContent = _organizationHTMLGenerator.GenerateHtml(organization);
			var pdfBytes = _pdfGenerator.GeneratePdf(htmlContent);

			return new ResultOrganizationPDF { PdfContent = pdfBytes, FileName = $"{organization.Name}_Report.pdf"};
		}
	}
}
