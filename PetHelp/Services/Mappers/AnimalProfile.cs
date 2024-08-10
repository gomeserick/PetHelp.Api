using AutoMapper;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;

namespace PetHelp.Services.Mappers
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<AnimalRequest, AnimalDto>();
        }
    }
}
