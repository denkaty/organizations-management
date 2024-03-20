using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Options
{
	public class DataOptions
	{
		public string CsvReadFolderPath { get; set; }
		public string JsonOutputFolderPath { get; set; }
		public string MovedCsvFolderPath { get; set; }
	}
}
