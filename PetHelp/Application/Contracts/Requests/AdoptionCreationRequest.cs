namespace PetHelp.Application.Contracts.Requests
{
    public class AdoptionCreationRequest
    {
        public int UserId { get; set; }
        public IEnumerable<AdoptionDetailCreationRequest> AdoptionDetails { get; set; }

    }
    public class AdoptionDetailCreationRequest
    {
        public int AnimalId { get; set; }
        public string Observation { get; set; }
    }
}
