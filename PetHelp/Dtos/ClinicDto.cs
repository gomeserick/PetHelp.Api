using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class ClinicDto : BaseDto
    {
        public string Address { get; set; }
        public string Cnpj { get; set; }
        public string License { get; set; }
        public string Name { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AnimalDto> Animals { get; set; }
    }
}
