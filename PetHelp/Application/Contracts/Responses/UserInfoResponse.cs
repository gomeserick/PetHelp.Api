namespace PetHelp.Application.Contracts.Responses
{
    public class UserInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Image { get; set; }
        public bool NotificationEnabled { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
