using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class ClientAnimalDto
    {
        public int AnimalId { get; set; }
        public int ClientId { get; set; }
        public int? AdoptionId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AnimalDto Animal { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClientDto Client { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AdoptionDto? Adoption { get; set; }
    }
}
