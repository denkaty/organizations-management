﻿using Organizations.Data.Abstraction.CRUD.Base;
using Organizations.Data.Abstraction.CRUD.BaseEntity;
using Organizations.Data.Models.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.CRUD
{
    public interface IEntityRepository<T> : IEntiyCreate<T>,
											IEntityRead<T>,
											IEntityReadMany<T>,
											IEntityUpdate<T>,
											IEntitySoftDelete,
											IEntityRestore
											where T : class, IEntity
	{
	}
}
