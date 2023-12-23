using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.BaseJunction
{
	public interface IJunctionDelete<T> where T : class, IJunction
	{
		void DeleteByFirstKey(string key);
		void DeleteBySecondKey(string key);
		void DeleteByCompositeKey(string key1, string key2);
	}
}
