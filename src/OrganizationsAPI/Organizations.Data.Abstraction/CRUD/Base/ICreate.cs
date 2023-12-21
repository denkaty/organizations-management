using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.Base
{
    public interface ICreate<T> where T : class
    {
        void Add(T entity);
    }
}
