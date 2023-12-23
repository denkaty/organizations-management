using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.Base
{
    public interface IEntityUpdate<T> where T : class, IEntity
	{
        void Update(string id, T entity);
    }
}
