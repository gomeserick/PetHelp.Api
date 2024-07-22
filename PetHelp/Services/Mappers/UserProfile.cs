using AutoMapper;
using PetHelp.Application.Contracts.Responses;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Context.Interfaces;

namespace PetHelp.Services.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityBaseDto, IContext>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(e => e.Id));

            CreateMap<IContext, UserInfoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.UserId));
        }
    }
}
