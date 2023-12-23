using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.BaseJunction
{
	public interface IJunctionCreate<T> where T : class, IJunction
	{
		void Insert(T entity);
	}
}
