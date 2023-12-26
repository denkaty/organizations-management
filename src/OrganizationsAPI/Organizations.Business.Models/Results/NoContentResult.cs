using Organizations.Business.Models.Enums;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Results
{
	public class NoContentResult<T> : IAPIResult<T>
	{
		public OrganizationsAPIStatusCode StatusCode => OrganizationsAPIStatusCode.NoContent;

		public T Data { get; }
	}
}
