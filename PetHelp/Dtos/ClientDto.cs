using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class ClientDto : BaseDto
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Email { get; set; }
        public bool Notification { get; set; }
        public string Address { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AnimalDto> Animals { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionDto> Adoptions { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ClientAnimalDto> ClientAnimals { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}
