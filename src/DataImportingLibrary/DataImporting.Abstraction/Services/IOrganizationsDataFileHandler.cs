using DataImporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Abstraction.Services
{
	public interface IOrganizationsDataFileHandler
	{
		bool CheckIfFolderExists(string folderPath);
		string[] GetCSVFiles(string folderPath);
		void CreateJsonFile(string jsonOutputFolderPath, string jsonData);
		void DeleteFile(string filePath);
		void MoveFile(string csvFilePath, string movedCsvFolderPath);
	}
}
