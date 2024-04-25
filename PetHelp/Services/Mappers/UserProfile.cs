using AutoMapper;
using PetHelp.Application.Contracts.Responses;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;

namespace PetHelp.Services.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityBaseDto, UserInfoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<ClientDto, UserInfoResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.RG, opt => opt.MapFrom(src => src.RG))
                .ForMember(dest => dest.NotificationEnabled, opt => opt.MapFrom(src => src.NotificationEnabled));

            CreateMap<EmployeeDto, UserInfoResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.RG, opt => opt.MapFrom(src => src.RG))
                .ForMember(dest => dest.NotificationEnabled, opt => opt.MapFrom(src => src.NotificationEnabled));
        }
    }
}
