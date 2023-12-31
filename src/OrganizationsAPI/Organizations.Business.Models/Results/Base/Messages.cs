using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Models.Results.Base
{
	public static class Messages
	{
		public const string ResourceUpdatedSuccessfully = "{0} '{1}' updated successfully.";
		public const string ResourceDeletedSuccessfully = "{0} '{1}' deleted successfully.";
		public const string ResourceNotFound = "Invalid request - {0} '{1}' not found.";
		public const string ResourceExists = "Invalid request - {0} '{1}' already exists.";
		public const string ResourceIsSoftDeleted = "Invalid request - {0} '{1}' is already soft deleted.";
		public const string ResourceIsNotSoftDeleted = "Invalid request - {0} '{1}' is not soft deleted.";
		public const string ResourceNameAlreadyExists = "Invalid request - {0} name {1} is already in use.";
		public const string ResourceDoesNotExist = "Invalid request - {0} '{1}' does not exist.";
		public const string ResourceIsNull = "Invalid request - {0} is null";
		public const string ResourceIsEmpty = "Invalid request - {0} is empty";
		public const string ResourceNameSameAsBefore = "Invalid request - The provided {0} name '{1}' matches its current name.";
		public const string ResourceIdSameAsBefore = "Invalid request - The provided {0} id '{1}' matches its current id.";
		public const string JunctionResourceExists = "Invalid request - {0} primary key '{1}' and '{2}' already exists.";
		public const string JunctionResourceDoesNotExists = "Invalid request - {0} primary key '{1}' and '{2}' does not exist.";


		public static string CountryIsSoftDeleted = "Invalid request - Country '{0}' is already soft deleted.";
		public static string IndustryDoesNotExist = "Invalid request - Industry '{0}' does not exist.";
		public static string OrganizationExists = "Invalid request - Organization '{0}' already exists.";
		public static string OrganizationNotFound = "Invalid request - Organization '{0}' not found.";

	}
}
