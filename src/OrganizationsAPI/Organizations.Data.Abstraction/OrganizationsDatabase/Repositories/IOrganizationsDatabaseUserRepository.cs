using Organizations.Data.Abstraction.CRUD.Base;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Abstraction.OrganizationsDatabase.Repositories
{
	public interface IOrganizationsDatabaseUserRepository
	{
		void Create(User entity);
		User? GetById(string id);
		User? GetByUsername(string username);
		User? GetByEmail(string email);
		User? GetByUsernameOrEmail(string identifier);
		ICollection<User> GetAll();
		IEnumerable<User> GetAll(Func<User, bool> predicate);
		//void UpdateByUsername(string username, User entity);
		void SoftDeleteByUsername(string username);
		void RestoreByUsername(string username);
	}
}
