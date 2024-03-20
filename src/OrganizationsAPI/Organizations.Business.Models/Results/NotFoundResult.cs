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
        public NotFoundResult(params string[] errorMessages)
        {
			ErrorMessages = errorMessages ?? Enumerable.Empty<string>();
		}

        public OrganizationsAPIStatusCode StatusCode => OrganizationsAPIStatusCode.NotFound;

		public IEnumerable<string> ErrorMessages { get; set; }

		public T Data { get; }

	}
}
