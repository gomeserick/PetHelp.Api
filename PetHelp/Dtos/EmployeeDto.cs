using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class EmployeeDto : BaseDto
    {
        public string Image { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public bool NotificationEnabled { get; set; }
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
