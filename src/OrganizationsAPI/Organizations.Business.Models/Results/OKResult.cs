using Organizations.Business.Models.Enums;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Results
{
	public class OKResult<T> : IAPIResult<T>
	{
		public OKResult(T data)
		{
			Data = data;
		}

		public OrganizationsAPIStatusCode StatusCode => OrganizationsAPIStatusCode.OK;
		public IEnumerable<string> ErrorMessages => Enumerable.Empty<string>();
		public T Data { get; }
	}
}
