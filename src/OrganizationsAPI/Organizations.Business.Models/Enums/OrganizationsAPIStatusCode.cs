using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Enums
{
	public enum OrganizationsAPIStatusCode
	{
		OK = 200,
		NoContent = 204,
		NotFound = 404,
		MethodNotAllowed = 405,
		BadRequest = 400,
		Unauthorized = 401,
		Forbidden = 403
	}
}
