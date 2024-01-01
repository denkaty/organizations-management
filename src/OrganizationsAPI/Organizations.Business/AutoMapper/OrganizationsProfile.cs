using AutoMapper;
using DataImporting.Models;
using Organizations.Business.Models.DTOs.Country;
using Organizations.Business.Models.DTOs.Industry;
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

            CreateMap<CreateIndustryDTO, Industry>().ReverseMap();
            CreateMap<Industry, ResultIndustryDTO>().ReverseMap();
            CreateMap<UpdateIndustryDTO, Industry>().ReverseMap();

			CreateMap<CreateOrganizationDTO, Organization>()
			   .ForMember(dest => dest.Id, opt => opt.Ignore())
			   .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.OrganizationId))
			   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			   .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
			   .ForMember(dest => dest.CountryId, opt => opt.Ignore())
			   .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			   .ForMember(dest => dest.Founded, opt => opt.MapFrom(src => src.Founded))
			   .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));
			CreateMap<Organization, ResultOrganizationDTO>().ReverseMap();
			CreateMap<UpdateOrganizationDTO, Organization>().ReverseMap();

			CreateMap<NormalizedOrganization, Organization>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.CountryId, opt => opt.Ignore());

			CreateMap<NormalizedOrganization, Country>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
		}
	}
}
