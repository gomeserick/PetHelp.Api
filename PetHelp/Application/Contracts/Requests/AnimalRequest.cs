namespace PetHelp.Application.Contracts.Requests
{
    public class AnimalRequest
    {
        public string Species { get; set; }
        public string Color { get; set; }
        public string Gender { get; set; }
        public string Temperament { get; set; }
        public string Image { get; set; }
        public int ClinicId { get; set; }
    }
}
