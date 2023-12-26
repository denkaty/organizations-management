using Organizations.Business.Models.Enums;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Results
{
	public class NotFoundResult<T> : IAPIResult<T>
	{
		public OrganizationsAPIStatusCode StatusCode => OrganizationsAPIStatusCode.NotFound;

		public T Data { get; }
	}
}
