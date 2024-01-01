
using Organizations.Business.Abstraction.Services;

namespace Organizations.Presentation.API.BackgroundServices
{
	public class RecurringDataImportingHostedService : IHostedService, IDisposable
	{
		private readonly IDataImportingManager _dataImportingManager;
		private Timer _timer;

		public RecurringDataImportingHostedService(IDataImportingManager dataImportingManager)
		{
			_dataImportingManager = dataImportingManager;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
			return Task.CompletedTask;
		}
		private void DoWork(object state)
		{
			_dataImportingManager.ProcessImporting();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
