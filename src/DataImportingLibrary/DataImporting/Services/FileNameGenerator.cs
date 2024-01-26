using DataImporting.Abstraction.Services;
using DataImporting.Models.Constants;

namespace DataImporting.Services
{
	public class FileNameGenerator : IFileNameGenerator
	{
		public string GenerateTimestamp()
		{
			return DateTime.UtcNow.ToString(FileConstants.TimestampFormat);
		}
		public string GenerateJsonDataFile()
		{
			string timestamp = GenerateTimestamp();

			string fileName = $"{timestamp}{FileConstants.JsonExtension}";

			return fileName;
		}

		public string GeneratedMovedCsvReadFile(string currentFileName)
		{
			string timestamp = GenerateTimestamp();

			return $"{Path.GetFileNameWithoutExtension(currentFileName)}_{timestamp}{Path.GetExtension(currentFileName)}";
		}
	}
}
