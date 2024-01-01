using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Abstraction.Services
{
	public interface IFileNameGenerator
	{
		public string GenerateTimestamp();
		public string GenerateJsonDataFile();
		public string GeneratedMovedCsvReadFile(string currentFileName);

	}
}
