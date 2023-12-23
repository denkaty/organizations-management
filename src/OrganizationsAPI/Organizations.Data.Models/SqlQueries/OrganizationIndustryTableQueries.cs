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
									(OrganizationId, IndustryId) 
                                    OrganizationIndustry VALUES
									(@OrganizationId, @IndustryId)";

		public const string GetByCompositeKey = @"SELECT * FROM OrganizationIndustry 
												     WHERE OrganizationId = @OrganizationId AND IndustryId = @IndustryId";

		public const string GetAll = @"SELECT * FROM OrganizationIndustry";

		public const string GetAllByOrganizationId = @"SELECT * FROM OrganizationIndustry 
													   WHERE OrganizationId = @OrganizationId";

		public const string GetAllByIndustryId = @"SELECT * FROM OrganizationIndustry 
												   WHERE IndustryId = @IndustryId";

		public const string UpdateByOrganizationId = @"UPDATE OrganizationIndustry 
													  SET OrganizationId = @OrganizationId, 
														  IndustryId = @IndustryId
                                                      WHERE OrganizationId = @OrganizationId";

		public const string UpdateByIndustryId = @"UPDATE OrganizationIndustry 
												   SET OrganizationId = @OrganizationId, 
													   IndustryId = @IndustryId
												   WHERE IndustryId = @IndustryId";

		public const string DeleteByOrganizationId = @"DELETE FROM OrganizationIndustry 
													   WHERE OrganizationId = @OrganizationId";

		public const string DeleteByIndustryId = @"DELETE FROM OrganizationIndustry 
												  WHERE IndustryId = @IndustryId";

		public const string DeleteByCompositeKey = @"DELETE FROM OrganizationIndustry 
												     WHERE OrganizationId = @OrganizationId AND IndustryId = @IndustryId";

		
	}
}
