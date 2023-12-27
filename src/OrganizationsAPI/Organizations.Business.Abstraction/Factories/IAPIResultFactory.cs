using Organizations.Business.Models;
using Organizations.Business.Models.Results;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Factories
{
	public interface IAPIResultFactory
	{
		IAPIResult<T> GetNotFoundResult<T>(params string[] errorMessages);
		IAPIResult<T> GetNoContentResult<T>();
		IAPIResult<T> GetOKResult<T>(T data = default);
		IAPIResult<T> GetBadRequestResult<T>(params string[] errorMessages);
	}
}
