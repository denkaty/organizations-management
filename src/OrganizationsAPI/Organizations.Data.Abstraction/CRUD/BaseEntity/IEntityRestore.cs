using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.BaseEntity
{
	public interface IEntityRestore
	{
		void RestoreById(string id);
	}
}
