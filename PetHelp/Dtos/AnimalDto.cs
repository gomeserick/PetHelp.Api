using PetHelp.Dtos.Base;
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
        [ReadOnly(true)]
        public ClinicDto Clinic { get; set; }
        [ReadOnly(true)]
        public IEnumerable<ClientDto> Clients { get; set; }
        [ReadOnly(true)]
        public IEnumerable<AdoptionDto> Adoptions { get; set; }
        [ReadOnly(true)]
        public IEnumerable<ClientAnimalDto> ClientAnimals { get; set; }
    }
}
