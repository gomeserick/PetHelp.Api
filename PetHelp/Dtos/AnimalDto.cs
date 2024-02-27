using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace PetHelp.Dtos
{
    public class AnimalDto : BaseDto
    {
        public string Species { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public string Temperament { get; set; }
        public string Image { get; set; }
        public int ClinicId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClinicDto Clinic { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ClientDto> Clients { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionDto> Adoptions { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ClientAnimalDto> ClientAnimals { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
        
    }
}
