namespace PetHelp.Application.Contracts.Requests
{
    public class ApointmentRequest
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public int ClinicId { get; set; }
        public IEnumerable<int> Animals { get; set; }
    }
}
