using DinkToPdf;
using DinkToPdf.Contracts;
using Organizations.Business.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class PDFGenerator : IPDFGenerator
	{
		private readonly IConverter _converter;
		public PDFGenerator(IConverter converter)
		{
			_converter = converter;
		}
		public byte[] GeneratePdf(string htmlContent)
		{
			var doc = new HtmlToPdfDocument
			{
				GlobalSettings = {
				PaperSize = PaperKind.A4,
				Orientation = Orientation.Portrait
			},
				Objects = {
				new ObjectSettings { HtmlContent = htmlContent }
			}
			};

			return _converter.Convert(doc);
		}
	}
}
