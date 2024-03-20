using AutoMapper;
using Organizations.Business.Abstraction.Factories;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Account;
using Organizations.Business.Models.DTOs.User;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class AccountService : IAccountService
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IAPIResultFactory _apiResultFactory;
		private readonly IMapper _mapper;
		private readonly IPasswordManager _passwordManager;
		private readonly ITokenGenerator _tokenManager;

		public AccountService(IOrganizationsContext organizationsContext,
							  IAPIResultFactory apiResultFactory,
							  IMapper mapper,
							  IPasswordManager passwordManager,
							  ITokenGenerator tokenManager)
		{
			_organizationsContext = organizationsContext;
			_apiResultFactory = apiResultFactory;
			_mapper = mapper;
			_passwordManager = passwordManager;
			_tokenManager = tokenManager;
		}

		public IAPIResult<ResultAccountDTO> Register(RegisterAccountDTO registerAccountDTO)
		{
			bool isUsernameExisting = _organizationsContext.Users.IsUsernameExisting(registerAccountDTO.Username);
			if (!isUsernameExisting)
			{
				return _apiResultFactory.GetBadRequestResult<ResultAccountDTO>(string.Format(Messages.UsernameAlreadyExists, registerAccountDTO.Username));
			}

			bool isEmailExisting = _organizationsContext.Users.IsEmailExisting(registerAccountDTO.Email);
			if (!isEmailExisting)
			{
				return _apiResultFactory.GetBadRequestResult<ResultAccountDTO>(string.Format(Messages.AccountEmailAlreadyExists, registerAccountDTO.Email));
			}

			User user = _mapper.Map<User>(registerAccountDTO);
			user.PasswordHash = _passwordManager.GenerateHashWithSalt(registerAccountDTO.Password, out string salt);
			user.Salt = salt;

			_organizationsContext.Users.Create(user);
			return _apiResultFactory.GetNoContentResult<ResultAccountDTO>();
		}

		public IAPIResult<LoginResultAccountDTO> Login(LoginAccountDTO loginAccountDTO)
		{
			User? existingUser = _organizationsContext.Users.GetByEmail(loginAccountDTO.Email);
			if (existingUser == null)
			{
				return _apiResultFactory.GetBadRequestResult<LoginResultAccountDTO>(string.Format(Messages.AccountInvalidLogin));
			}

			bool isPasswordCorrect = _passwordManager.VerifyPasswordHash(loginAccountDTO.Password, existingUser.PasswordHash, existingUser.Salt);
			if (!isPasswordCorrect)
			{
				return _apiResultFactory.GetBadRequestResult<LoginResultAccountDTO>(string.Format(Messages.AccountInvalidLogin));
			}

			string token = _tokenManager.GenerateToken(existingUser.Username, existingUser.IsAdmin);
			LoginResultAccountDTO loginResultAccountDTO = new LoginResultAccountDTO { Token = token, };
			return _apiResultFactory.GetOKResult(loginResultAccountDTO);
		}



	}
}
