using Organizations.Data.Models.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.BaseJunction
{
    public interface IJunctionReadMany<T> where T : class, IJunction
	{
		ICollection<T> GetByFirstKey(string key);
		ICollection<T> GetBySecondKey(string key);
		ICollection<T> GetAll();
		IEnumerable<T> GetAll(Func<T, bool> predicate);

	}
}
