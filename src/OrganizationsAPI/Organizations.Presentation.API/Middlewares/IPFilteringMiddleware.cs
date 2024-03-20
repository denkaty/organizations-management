using Microsoft.Extensions.Options;
using Organizations.Business.Models.Options;

namespace Organizations.Presentation.API.Middlewares
{
	public class IPFilteringMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly HostsOptions _hostsOptions;

		public IPFilteringMiddleware(RequestDelegate next, IOptions<HostsOptions> hostsOptions)
		{
			_next = next;
			_hostsOptions = hostsOptions.Value;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			var clientIP = context.Connection.RemoteIpAddress;

			try
			{
				if (!_hostsOptions.Allowed.Contains(clientIP.ToString()))
				{
					context.Response.StatusCode = 403;
					await context.Response.WriteAsync("Access Denied");
					return;
				}
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync($"Error checking IP address: {ex.Message}");
				return;
			}

			await _next(context);
		}

	}

}