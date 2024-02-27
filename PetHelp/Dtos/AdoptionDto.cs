using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace PetHelp.Dtos
{
    public class AdoptionDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string Observation { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClientDto Client { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AnimalDto> Animals { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ClientAnimalDto> ClientAnimals { get; set; }
    }
}
