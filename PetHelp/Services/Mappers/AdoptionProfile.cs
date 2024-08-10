using AutoMapper;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;

namespace PetHelp.Services.Mappers
{
    public class AdoptionProfile: Profile
    {
        public AdoptionProfile()
        {
            CreateMap<AdoptionCreationRequest, AdoptionHeaderDto>();
            CreateMap<AdoptionDetailCreationRequest, AdoptionDetailDto>();
        }
    }
}
