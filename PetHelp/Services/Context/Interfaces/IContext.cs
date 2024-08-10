namespace PetHelp.Services.Context.Interfaces
{
    public interface IContext
    {
        int UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string CPF { get; set; }
        string RG { get; set; }
        string Image { get; set; }
        bool NotificationEnabled { get; set; }
        bool IsEmployee { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}
