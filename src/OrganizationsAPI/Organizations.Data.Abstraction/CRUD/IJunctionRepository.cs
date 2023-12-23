using Organizations.Data.Abstraction.CRUD.BaseJunction;
using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD
{
	public interface IJunctionRepository<T> : IJunctionCreate<T>,
											  IJunctionRead<T>,
											  IJunctionReadMany<T>,
											  IJunctionUpdate<T>,
											  IJunctionDelete<T>
											  where T : class, IJunction
	{
	}
}
