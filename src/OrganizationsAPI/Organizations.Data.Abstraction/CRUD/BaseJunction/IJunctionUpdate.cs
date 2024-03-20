using Organizations.Data.Models.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.BaseJunction
{
    public interface IJunctionUpdate<T> where T : class, IJunction
	{
		void UpdateByFirstKey(string key, T entity);
		void UpdateBySecondKey(string key, T entity);
	}
}
