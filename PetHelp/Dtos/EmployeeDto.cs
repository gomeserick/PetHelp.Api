using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string RG { get; set; }
        public string Image { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionDto> Adoption { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
    }
}
