using CsvHelper;
using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class CSVReader : ICSVReader
	{
		private readonly IOrganizationDataNormalizer _organizationDataNormalizer;

		public CSVReader(IOrganizationDataNormalizer organizationDataNormalizer)
		{
			_organizationDataNormalizer = organizationDataNormalizer;
		}

		public string? ReadCSVData(string csvFilePath)
		{
			string json = null;
			try
			{
				using var streamReader = new StreamReader(csvFilePath);
				using var csvReader = new CsvReader(streamReader, culture: CultureInfo.InvariantCulture);

				IEnumerable<RawOrganization> rawOrganizations = csvReader.GetRecords<RawOrganization>();

				IEnumerable<NormalizedOrganization> normalizedOrganizations = _organizationDataNormalizer.NormalizeOrganizationData(rawOrganizations);

				json = JsonConvert.SerializeObject(normalizedOrganizations);
			}
			catch (Exception)
			{
				throw;
			}

			return json;
		}
	}
}
