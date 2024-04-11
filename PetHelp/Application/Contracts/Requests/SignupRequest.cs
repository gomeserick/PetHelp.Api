namespace PetHelp.Application.Contracts.Requests
{
    public class SignupRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }
    }
}
