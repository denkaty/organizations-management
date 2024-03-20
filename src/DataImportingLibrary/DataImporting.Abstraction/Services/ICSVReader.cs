using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Abstraction.Services
{
	public interface ICSVReader
	{
		string? ReadCSVData(string csvFilePath);
	}
}
