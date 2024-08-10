using PetHelp.Services.Context.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PetHelp.Services.Context
{
    public class PetHelpContext : IContext
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Image { get; set; }
        public bool NotificationEnabled { get; set; }
        public bool IsEmployee { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}
