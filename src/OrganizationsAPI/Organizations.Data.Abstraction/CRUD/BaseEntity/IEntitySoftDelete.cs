using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD.Base
{
    public interface IEntitySoftDelete
    {
        void SoftDeleteById(string id);
    }
}
