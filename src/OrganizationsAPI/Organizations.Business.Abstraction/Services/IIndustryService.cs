using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
	public interface IIndustryService
	{
		IAPIResult<ResultIndustryDTO> Create(CreateIndustryDTO createIndustryDTO);
		IAPIResult<ResultIndustryDTO> GetById(string id);
		IAPIResult<ResultIndustryDTO> GetByName(string name);
		IAPIResult<ICollection<ResultIndustryDTO>> GetAll();
		IAPIResult<ResultIndustryDTO> UpdateById(string id, UpdateIndustryDTO updateIndustryDTO);
		IAPIResult<ResultIndustryDTO> SoftDeleteById(string id);
		IAPIResult<ResultIndustryDTO> RestoreById(string id); 
	}
}
