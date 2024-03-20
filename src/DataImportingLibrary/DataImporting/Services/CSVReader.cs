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
		public string? ReadCSVData(string csvFilePath)
		{

			try
			{
				using (var streamReader = new StreamReader(csvFilePath))
				using (var csvReader = new CsvReader(streamReader, culture: CultureInfo.InvariantCulture))
				{
					IEnumerable<RawOrganization> rawOrganizations = csvReader.GetRecords<RawOrganization>();

					return JsonConvert.SerializeObject(rawOrganizations);
				}
			}
			catch (Exception)
			{
				throw;
			}

		}

	}
}
