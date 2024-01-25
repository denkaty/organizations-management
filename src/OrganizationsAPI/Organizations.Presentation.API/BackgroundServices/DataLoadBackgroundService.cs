
using DataImporting.Abstraction.Services;
using Organizations.Data.Abstraction.DatabaseContexts;

namespace Organizations.Presentation.API.BackgroundServices
{
	public class DataLoadBackgroundService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ICachedOrganizationsIdsHelper _cachedOrganizationsIdsHelper;
        public DataLoadBackgroundService(IServiceProvider serviceProvider, 
									     ICachedOrganizationsIdsHelper organizationsIdsHelper)
        {
			_serviceProvider = serviceProvider;
			_cachedOrganizationsIdsHelper = organizationsIdsHelper;
		}
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var organizationsContext = scope.ServiceProvider.GetRequiredService<IOrganizationsContext>();

				_cachedOrganizationsIdsHelper.LoadData(organizationsContext);
			}

			return Task.CompletedTask;
		}
	}
}
