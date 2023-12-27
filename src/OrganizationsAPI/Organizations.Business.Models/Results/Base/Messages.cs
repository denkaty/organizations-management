using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Results.Base
{
	public static class Messages
	{
		public const string UpdatedSuccessfully = "Resource updated successfully.";
		public const string DeletedSuccessfully = "Resource deleted successfully.";
		public const string NotFound = "Resource not found.";
		public static string CountryDoesNotExist = "Invalid request - Country '{0}' does not exist.";
		public static string IndustryDoesNotExist = "Invalid request - Industry '{0}' does not exist.";
		public static string OrganizationExists = "Invalid request - Organization '{0}' already exists.";
		public static string OrganizationNotFound = "Invalid request - Organization '{0}' not found.";
	}
}
