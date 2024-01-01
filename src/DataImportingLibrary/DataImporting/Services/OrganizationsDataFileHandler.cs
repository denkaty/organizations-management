using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Organizations.Business.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class OrganizationsDataFileHandler : IOrganizationsDataFileHandler
	{
		private readonly IFileNameGenerator _fileNameGenerator;

		public OrganizationsDataFileHandler(IFileNameGenerator fileNameGenerator)
		{
			_fileNameGenerator = fileNameGenerator;
		}

		public bool CheckIfFolderExists(string folderPath)
		{
			return Directory.Exists(folderPath);
		}
		public string[] GetCSVFiles(string folderPath)
		{
			return Directory.GetFiles(folderPath, "*.txt");
		}

		public void CreateJsonFile(string jsonOutputFolderPath, string jsonData)
		{
			try
			{
				string jsonFilePath = Path.Combine(jsonOutputFolderPath, _fileNameGenerator.GenerateJsonDataFile());

				File.WriteAllText(jsonFilePath, jsonData);
				Console.WriteLine($"JSON file created at: {jsonFilePath}");
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public void MoveFile(string currentCsvFilePath, string movedCsvFolderPath)
		{
			try
			{
				string movedCsvFilePath = Path.Combine(movedCsvFolderPath, _fileNameGenerator.GeneratedMovedCsvReadFile(currentCsvFilePath));

				File.Move(currentCsvFilePath, movedCsvFilePath);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void DeleteFile(string filePath)
		{
			try
			{
				File.Delete(filePath);
			}
			catch (Exception)
			{
				throw;
			}

		}

	}
}
