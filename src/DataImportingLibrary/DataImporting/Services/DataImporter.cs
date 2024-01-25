using AutoMapper;
using DataImporting.Abstraction.Services;
using DataImporting.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.Results.Base;
using Organizations.Data.Abstraction.DatabaseContexts;
using Organizations.Data.Models.CustomAttributes;
using Organizations.Data.Models.Entities;
using Organizations.Data.Models.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class DataImporter : IDataImporter
	{
		private readonly string _connectionString;
		public DataImporter(IOptions<OrganizationsDatabaseOptions> options)
		{
			_connectionString = options.Value.ConnectionString;
		}

		public void ImportData(BulkedDataWrapper bulkedDataWrapper)
		{
			ProcessBulkInsert(bulkedDataWrapper.BulkedCountries);
			ProcessBulkInsert(bulkedDataWrapper.BulkedIndustries);
			ProcessBulkInsert(bulkedDataWrapper.BulkedOrganizations);
			ProcessBulkInsert(bulkedDataWrapper.BulkedOrganizationsIndustries);
		}
		private void ProcessBulkInsert<T>(ICollection<T> data) where T : class
		{
			BulkInsert<T>(ConvertToDataTable(data));
		}
		private DataTable ConvertToDataTable<T>(ICollection<T> data) where T : class
		{
			DataTable dataTable = new();

			if (data == null || data.Count == 0)
			{
				return dataTable;
			}

			var properties = typeof(T)
				.GetProperties()
				.OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false).FirstOrDefault())?.Order ?? int.MaxValue)
				.ToArray();

			foreach (var property in properties)
			{
				dataTable.Columns.Add(property.Name);
			}

			foreach (var item in data)
			{
				DataRow row = dataTable.NewRow();

				foreach (var property in properties)
				{
					row[property.Name] = property.GetValue(item) ?? DBNull.Value;
				}

				dataTable.Rows.Add(row);
			}

			return dataTable;
		}
		private void BulkInsert<T>(DataTable dataTable) where T : class
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				SqlTransaction sqlTransaction = connection.BeginTransaction();

				using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, sqlTransaction))
				{
					bulkCopy.DestinationTableName = typeof(T).Name;
					bulkCopy.BulkCopyTimeout = 0;

					bulkCopy.WriteToServer(dataTable);
				}

				sqlTransaction.Commit();
			}
		}

		
	}

}