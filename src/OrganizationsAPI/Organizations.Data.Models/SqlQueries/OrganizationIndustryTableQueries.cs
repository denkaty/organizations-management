using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Data.Models.SqlQueries
{
	public class OrganizationIndustryTableQueries
	{
		public const string Add = @"INSERT INTO OrganizationIndustry
									(Organization_Id, Industry_Id) VALUES
									(@Organization_Id, @Industry_Id)";

		public const string GetByCompositeKey = @"SELECT * FROM OrganizationIndustry 
												     WHERE Organization_Id = @Organization_Id AND Industry_Id = @Industry_Id";

		public const string GetAll = @"SELECT * FROM OrganizationIndustry";

		public const string GetAllByOrganizationId = @"SELECT * FROM OrganizationIndustry 
													   WHERE Organization_Id = @Organization_Id";

		public const string GetAllByIndustryId = @"SELECT * FROM OrganizationIndustry 
												   WHERE Industry_Id = @Industry_Id";

		public const string UpdateByOrganizationId = @"UPDATE OrganizationIndustry 
													  SET Organization_Id = @Organization_Id, 
														  Industry_Id = @Industry_Id
                                                      WHERE Organization_Id = @Organization_Id";

		public const string UpdateByIndustryId = @"UPDATE OrganizationIndustry 
												   SET Organization_Id = @Organization_Id, 
													   Industry_Id = @Industry_Id
												   WHERE Industry_Id = @Industry_Id";

		public const string DeleteByOrganizationId = @"DELETE FROM OrganizationIndustry 
													   WHERE Organization_Id = @Organization_Id";

		public const string DeleteByIndustryId = @"DELETE FROM OrganizationIndustry 
												  WHERE Industry_Id = @Industry_Id";

		public const string DeleteByCompositeKey = @"DELETE FROM OrganizationIndustry 
												     WHERE Organization_Id = @Organization_Id AND Industry_Id = @Industry_Id";

		
	}
}
