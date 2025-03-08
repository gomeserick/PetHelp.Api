namespace PetHelp.Application.Contracts.Requests
{
    public class FullRegistrationRequest
    {
        public string Name { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public bool NotificationEnabled { get; set; }
    }
}