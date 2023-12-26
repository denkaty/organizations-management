using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizations.Data.Models.Entities.Base.Interfaces;

namespace Organizations.Data.Models.Entities.Base
{
    public class Entity : IEntity
	{
		public Entity()
		{
			Id = Guid.NewGuid().ToString();
		}

		public string Id { get; set; }
	}
}
