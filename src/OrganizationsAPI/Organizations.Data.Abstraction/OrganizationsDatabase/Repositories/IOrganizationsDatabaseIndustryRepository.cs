﻿using Organizations.Data.Abstraction.CRUD;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.OrganizationsDatabase.Repositories
{
	public interface IOrganizationsDatabaseIndustryRepository : IEntityRepository<Industry>
	{
		Industry? GetByName(string name);
	}
}
