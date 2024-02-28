using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class MessageDto: BaseDto
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClientDto Client { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
    }
}
