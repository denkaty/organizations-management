using Organizations.Data.Models.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.Base
{
    public interface IEntityReadMany<T> where T : class, IEntity
	{
        ICollection<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> predicate);
	}
}
