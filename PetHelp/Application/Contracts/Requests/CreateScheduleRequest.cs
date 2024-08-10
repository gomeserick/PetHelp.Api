namespace PetHelp.Application.Contracts.Requests
{
    public class CreateScheduleRequest
    {
        public int AnimalId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
    }
}
