using AutoMapper;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Data.Abstraction.DatabaseContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
    public class OrganizationService : IOrganizationService
	{
		private readonly IOrganizationsContext _organizationsContext;
		private readonly IMapper _mapper;
		public OrganizationService(IOrganizationsContext organizationsContext, 
									IMapper mapper)
		{
			_organizationsContext = organizationsContext;
			_mapper = mapper;
		}

		public void Create(CreateOrganizationDTO createOrganizationDTO)
		{
			throw new NotImplementedException();
		}

		public void ReadAll()
		{

		}

	}
}
