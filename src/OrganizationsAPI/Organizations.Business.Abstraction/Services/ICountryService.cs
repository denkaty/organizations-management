using Organizations.Business.Models;
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
	public interface ICountryService
	{
		IAPIResult<ResultCountryDTO> Create(CreateCountryDTO createCountryDTO);
		IAPIResult<ResultCountryDTO> GetById(string id);
		IAPIResult<ResultCountryDTO> GetByName(string name);
		IAPIResult<ICollection<ResultCountryDTO>> GetAll();
		IAPIResult<ResultCountryDTO> UpdateById(string id, UpdateCountryDTO updateCountryDTO);
		IAPIResult<ResultCountryDTO> SoftDeleteById(string id);
		IAPIResult<ResultCountryDTO> RestoreById(string id);

	}
}
