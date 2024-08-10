using AutoMapper;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;
using PetHelp.Services.Context.Interfaces;

namespace PetHelp.Services.Mappers
{
    public class ApointmentProfile: Profile
    {
        public ApointmentProfile(IContext context)
        {
            CreateMap<ApointmentRequest, ApointmentHeaderDto>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Animals.Select(e => new ApointmentDetailDto()
                {
                    UserId = context.UserId,
                    AnimalId = e
                })));
        }
    }
}
