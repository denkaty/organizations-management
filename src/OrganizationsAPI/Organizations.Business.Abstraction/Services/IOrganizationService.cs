using Organizations.Business.Models.DTOs.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
    public interface IOrganizationService
	{
		void Create(CreateOrganizationDTO createOrganizationDTO);
	}
}
