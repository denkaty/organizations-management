using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class OrganizationHTMLGenerator : IOrganizationHTMLGenerator
	{
		private readonly StringBuilder _htmlBuilder;

		public OrganizationHTMLGenerator()
		{
			_htmlBuilder = new StringBuilder();
		}

		public string GenerateHtml(ResultOrganizationDTO organization)
		{
			_htmlBuilder.Clear();

			_htmlBuilder.Append("<html>");
			_htmlBuilder.Append("<head><title>Organization Report</title></head>");
			_htmlBuilder.Append("<body>");

			_htmlBuilder.Append($"<h1>Organization Name: {organization.Name}</h1>");
			_htmlBuilder.Append($"<p>Website: {organization.Website}</p>");
			_htmlBuilder.Append($"<p>Description: {organization.Description}</p>");
			_htmlBuilder.Append($"<p>Founded: {organization.Founded}</p>");
			_htmlBuilder.Append($"<p>Employees: {organization.Employees}</p>");
			_htmlBuilder.Append($"<p>Country: {organization.Country}</p>");

			_htmlBuilder.Append("<p>Industries:</p><ul>");
			foreach (var industry in organization.Industries)
			{
				_htmlBuilder.Append($"<li>{industry}</li>");
			}
			_htmlBuilder.Append("</ul>");

			_htmlBuilder.Append("</body>");
			_htmlBuilder.Append("</html>");

			return _htmlBuilder.ToString();
		}
	}
}
