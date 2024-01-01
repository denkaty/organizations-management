using DataImporting.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class FileNameGenerator : IFileNameGenerator
	{

		public string GenerateTimestamp()
		{
			return DateTime.UtcNow.ToString("yyyyMMddHHmmss");
		}
		public string GenerateJsonDataFile()
		{
			string fileName = "Data";
			string timestamp = GenerateTimestamp();
			string jsonExtension = ".json";

			return $"{fileName}_{timestamp}{jsonExtension}";

		}

		public string GeneratedMovedCsvReadFile(string currentFileName)
		{
			string timestamp = GenerateTimestamp();

			return $"{Path.GetFileNameWithoutExtension(currentFileName)}_{timestamp}{Path.GetExtension(currentFileName)}";
		}
	}
}
