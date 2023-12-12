using PetHelp.Dtos.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetHelp.Dtos
{
    public class AdoptionDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string Observation { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        [ReadOnly(true)]
        public ClientDto Client { get; set; }
        [ReadOnly(true)]
        public EmployeeDto Employee { get; set; }
        [ReadOnly(true)]
        public IEnumerable<AnimalDto> Animals { get; set; }
        [ReadOnly(true)]
        public IEnumerable<ClientAnimalDto> ClientAnimals { get; set; }
    }
}
