using Organizations.Data.Abstraction.CRUD.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD
{
	public interface IRepository<T> : ICreate<T>,
									  IRead<T>,
									  IReadMany<T>,
									  IUpdate<T>,
									  IDelete 
									  where T : class
	{
	}
}
