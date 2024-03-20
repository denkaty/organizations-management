using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Models
{
	public class BulkedDataWrapper
	{
		 public BulkedDataWrapper(){
            BulkedOrganizations = new List<Organization>();
            BulkedCountries = new List<Country>();
            BulkedIndustries = new List<Industry>();
            BulkedOrganizationsIndustries = new List<OrganizationIndustry>();
        }

	public ICollection<Organization> BulkedOrganizations { get; set; }
	public ICollection<Country> BulkedCountries { get; set; }
	public ICollection<Industry> BulkedIndustries { get; set; }
	public ICollection<OrganizationIndustry> BulkedOrganizationsIndustries { get; set; }
}
}
