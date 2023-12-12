using PetHelp.Dtos.Base;
using System.Reflection.Metadata;

namespace PetHelp.Dtos
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string RG { get; set; }
        public string Image { get; set; }
        public IEnumerable<AdoptionDto> Adoption { get; set; }
    }
}
