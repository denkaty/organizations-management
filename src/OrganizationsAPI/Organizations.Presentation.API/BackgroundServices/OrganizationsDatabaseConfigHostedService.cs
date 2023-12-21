using Organizations.Data.Abstraction;

namespace Organizations.Presentation.API.BackgroundServices
{
	public class OrganizationsDatabaseConfigHostedService : BackgroundService
	{
		private readonly IOrganizationsDatabaseConfigurator _configurator;

		public OrganizationsDatabaseConfigHostedService(IOrganizationsDatabaseConfigurator configurator)
		{
			_configurator = configurator;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_configurator.ConfigureDatabase();

			return Task.CompletedTask;
		}

	}
}
