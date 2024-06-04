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
            CreateMap<IdentityBaseDto, UserInfoResponse>();
        }
    }
}
