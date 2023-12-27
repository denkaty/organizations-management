using Microsoft.AspNetCore.Mvc;
using Organizations.Business.Models;
using Organizations.Business.Models.Enums;
using Organizations.Business.Models.Results.Base;
using System.Net;

namespace Organizations.Presentation.API.Extensions
{
	public static class ControllerExtensions
	{
		public static IActionResult HandleResponse<T>(this ControllerBase controller, IAPIResult<T> apiResult)
		{
			switch (apiResult.StatusCode)
			{
				case OrganizationsAPIStatusCode.NotFound:
					return controller.NotFound(apiResult.ErrorMessages);

				case OrganizationsAPIStatusCode.OK:
					return controller.Ok(apiResult.Data);

				case OrganizationsAPIStatusCode.NoContent:
					return controller.NoContent();

				case OrganizationsAPIStatusCode.BadRequest:
					return controller.BadRequest(apiResult.ErrorMessages);

				default:
					throw new Exception();
			}
		}

	}
}
