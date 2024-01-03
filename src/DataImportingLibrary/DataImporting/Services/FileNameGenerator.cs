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
			string timestamp = GenerateTimestamp();
			string jsonExtension = ".json";

			string fileName = $"{timestamp}{jsonExtension}";

			return fileName;
		}

		public string GeneratedMovedCsvReadFile(string currentFileName)
		{
			string timestamp = GenerateTimestamp();

			return $"{Path.GetFileNameWithoutExtension(currentFileName)}_{timestamp}{Path.GetExtension(currentFileName)}";
		}
	}
}
