using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
    public interface IOrganizationService
	{
		public IAPIResult<ResultOrganizationDTO> Create(CreateOrganizationDTO createOrganizationDTO);
		public IAPIResult<ResultOrganizationDTO> GetById(string id);
		public IAPIResult<ResultOrganizationDTO> GetByName(string name);
		public IAPIResult<ICollection<ResultOrganizationDTO>> GetAll();
		public IAPIResult<ResultOrganizationDTO> UpdateById(string id, UpdateOrganizationDTO updateOrganizationDTO);
		public IAPIResult<ResultOrganizationDTO> UpdateCountry(string organizationId, PatchCountryDTO patchCountryDTO);
		public IAPIResult<ResultOrganizationDTO> DeleteById(string id);
		public IAPIResult<ResultOrganizationDTO> RestoreById(string id);
		public IAPIResult<ResultOrganizationDTO> AddIndustry(string organizationId, AddIndustryDTO addIndustryDTO);
		public IAPIResult<ResultOrganizationDTO> RemoveIndustry(string id, string name);
	}
}
