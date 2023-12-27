using Organizations.Business.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Results.Base
{
    public interface IAPIResult<T>
    {
        OrganizationsAPIStatusCode StatusCode { get; }
		IEnumerable<string> ErrorMessages { get; }
		T Data { get; }
    }
}
