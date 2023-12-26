using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Enums
{
	public enum OrganizationsAPIStatusCode
	{
		OK,
		Created,
		NoContent,
		NotFound,
		BadRequest,
		Unauthorized,
		Forbidden
	}
}
