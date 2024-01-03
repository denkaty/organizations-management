using Organizations.Business.Models.DTOs.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
	public interface IDataExportManager
	{
		ResultOrganizationPDF? ProcessGeneratingOrganizationPDF(string id);
	}
}
