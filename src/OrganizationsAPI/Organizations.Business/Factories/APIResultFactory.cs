using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Models.Results;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Factories
{
	public class APIResultFactory : IAPIResultFactory
	{
		public IAPIResult<T> GetNoContentResult<T>()
		{
			return new NoContentResult<T>();
		}

		public IAPIResult<T> GetNotFoundResult<T>()
		{
			return new NotFoundResult<T>();
		}

		public IAPIResult<T> GetOKResult<T>(T data = default)
		{
			return new OKResult<T>(data);
		}
	}
}
