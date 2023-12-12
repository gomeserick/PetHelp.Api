using PetHelp.Dtos.Base;
using System.ComponentModel.DataAnnotations;

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
        public IEnumerable<AnimalDto> Animals { get; set; }
        public IEnumerable<AdoptionDto> Adoptions { get; set; }
        public IEnumerable<ClientAnimalDto> ClientAnimals { get; set; }
    }
}
