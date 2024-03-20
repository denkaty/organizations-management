using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.DTOs.Export
{
	public class ResultOrganizationPDF
	{
		public byte[] PdfContent { get; set; }
		public string FileName { get; set; }
	}
}
