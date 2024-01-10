using AutoMapper;
using CsvHelper;
using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.Options;
using System.Diagnostics;

namespace Organizations.Business.Services
{
	public class DataImportingManager : IDataImportingManager
	{
		private readonly DataOptions _dataOptions;
		private readonly ICSVReader _csvReader;
		private readonly IDataImporter _dataImporter;
		private readonly IMapper _mapper;
		private readonly IOrganizationsDataFileHandler _dataFileHandler;
		private readonly IReadDataSummarizer _readDataSummarizer;
		public DataImportingManager(IOptions<DataOptions> dataOptions,
									ICSVReader csvReader,
									IDataImporter dataImporter,
									IMapper mapper,
									IOrganizationsDataFileHandler dataFileHandler,
									IReadDataSummarizer readDataSummarizer)
		{
			_dataOptions = dataOptions.Value;
			_csvReader = csvReader;
			_dataImporter = dataImporter;
			_mapper = mapper;
			_dataFileHandler = dataFileHandler;
			_readDataSummarizer = readDataSummarizer;
		}
		public void ProcessImporting()
		{
			 if (!_dataFileHandler.CheckIfFolderExists(_dataOptions.CsvReadFolderPath))
            {
                throw new DirectoryNotFoundException();
            }

			string[] csvFiles = _dataFileHandler.GetCSVFiles(_dataOptions.CsvReadFolderPath);
			foreach (string csvFilePath in csvFiles)
			{
				var jsonData = _csvReader.ReadCSVData(csvFilePath);
				if (jsonData != null)
				{
                    ICollection<NormalizedOrganization> normalizedOrganizations = JsonConvert.DeserializeObject<ICollection<NormalizedOrganization>>(jsonData);

					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					_dataImporter.ImportData(normalizedOrganizations);
					stopwatch.Stop();
					long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
					Console.WriteLine($"Time taken to import data: {elapsedMilliseconds}ms");

					var readDataSummaryDTO = _readDataSummarizer.CreateSummary(normalizedOrganizations);

					_dataFileHandler.CreateJsonFile(_dataOptions.JsonOutputFolderPath, JsonConvert.SerializeObject(readDataSummaryDTO));

					_dataFileHandler.MoveFile(csvFilePath, _dataOptions.MovedCsvFolderPath);

					_dataFileHandler.DeleteFile(csvFilePath);
				}

			}
		}

	}
}
