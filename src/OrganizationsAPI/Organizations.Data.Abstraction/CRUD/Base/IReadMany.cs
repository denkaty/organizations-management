using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.Base
{
    public interface IReadMany<T> where T : class
    {
        ICollection<T> GetAll();
        ICollection<T> GetAll(Func<T, bool> predicate);
	}
}
