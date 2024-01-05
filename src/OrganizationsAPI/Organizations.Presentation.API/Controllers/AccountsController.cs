using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Account;
using Organizations.Business.Models.DTOs.User;
using Organizations.Presentation.API.Extensions;

namespace Organizations.Presentation.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountsController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost]
		[Route("SignUp")]
		public IActionResult SignUp(RegisterAccountDTO request)
		{
			var response = _accountService.SignUp(request);

			return this.HandleResponse(response);
		}

		[HttpPost]
		[Route("Login")]
		public IActionResult Login(LoginAccountDTO request)
		{
			var response = _accountService.Login(request);

			return this.HandleResponse(response);
		}

	}
}
