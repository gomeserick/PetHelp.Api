using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos.Identity
{
    public class IdentityBaseDto: IdentityUser<int>
    {
        public string Name { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public bool NotificationEnabled { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public UserDto User { get; set; }
    }
}
