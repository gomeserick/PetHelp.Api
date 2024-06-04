using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class EmployeeDto : BaseDto
    {
        public int UserId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IdentityBaseDto User { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionDto> Adoption { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}
