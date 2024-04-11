using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos.Identity
{
    public class IdentityBaseDto: IdentityUser<int>
    {
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClientDto Client { get; set; }
    }
}
