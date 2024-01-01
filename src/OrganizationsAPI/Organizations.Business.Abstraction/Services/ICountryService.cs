using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.Results.Base;

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
