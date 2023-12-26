using AutoMapper;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Organization;
using Organizations.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.AutoMapper
{
    public class OrganizationsProfile : Profile
	{
        public OrganizationsProfile()
        {
			CreateMap<CreateCountryDTO, Country>().ReverseMap();
            CreateMap<Country, ResultCountryDTO>().ReverseMap();
            CreateMap<UpdateCountryDTO, Country>().ReverseMap();

			CreateMap<CreateOrganizationDTO, Organization>().ReverseMap();
        }
    }
}
