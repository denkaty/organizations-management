using Organizations.Business.Models.DTOs.Account;
using Organizations.Business.Models.DTOs.User;
using Organizations.Business.Models.Results.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
	public interface IAccountService
	{
		IAPIResult<ResultAccountDTO> SignUp(RegisterAccountDTO registerAccountDTO);
		IAPIResult<LoginResultAccountDTO> Login(LoginAccountDTO loginAccountDTO);
	}
}
