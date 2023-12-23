using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.BaseJunction
{
	public interface IJunctionReadMany<T> where T : class, IJunction
	{
		ICollection<T> GetByKey1(string key);
		ICollection<T> GetByKey2(string key);
		ICollection<T> GetAll();
	}
}
