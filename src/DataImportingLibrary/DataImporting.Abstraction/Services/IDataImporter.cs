using DataImporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Abstraction.Services
{
	public interface IDataImporter
	{
		void ImportData(BulkedDataWrapper bulkedDataWrapper);
	}
}
