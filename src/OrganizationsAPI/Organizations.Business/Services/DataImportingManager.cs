using AutoMapper;
using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.Options;

namespace Organizations.Business.Services
{
	public class DataImportingManager : IDataImportingManager
	{
		private readonly DataOptions _dataOptions;
		private readonly ICSVReader _csvReader;
		private readonly IDataImporter _dataImporter;
		private readonly IMapper _mapper;
		private readonly IOrganizationsDataFileHandler _dataFileHandler;
		public DataImportingManager(IOptions<DataOptions> dataOptions,
									ICSVReader csvReader,
									IDataImporter dataImporter,
									IMapper mapper,
									IOrganizationsDataFileHandler dataFileHandler)
		{
			_dataOptions = dataOptions.Value;
			_csvReader = csvReader;
			_dataImporter = dataImporter;
			_mapper = mapper;
			_dataFileHandler = dataFileHandler;
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
					_dataImporter.ImportData(normalizedOrganizations);

					_dataFileHandler.CreateJsonFile(_dataOptions.JsonOutputFolderPath, jsonData);

					_dataFileHandler.MoveFile(csvFilePath, _dataOptions.MovedCsvFolderPath);

					_dataFileHandler.DeleteFile(csvFilePath);
				}

			}
		}

	}
}
