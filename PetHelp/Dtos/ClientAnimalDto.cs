namespace PetHelp.Dtos
{
    public class ClientAnimalDto
    {
        public int AnimalId { get; set; }
        public int ClientId { get; set; }
        public int? AdoptionId { get; set; }
        public AnimalDto Animal { get; set; }
        public ClientDto Client { get; set; }
        public AdoptionDto? Adoption { get; set; }
    }
}
