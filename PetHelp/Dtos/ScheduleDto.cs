using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class ScheduleDto: BaseDto
    {
        public int EmployeeId { get; set; }
        public int AnimalId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } // in minutes
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AnimalDto Animal { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClientDto Client { get; set; }
    }
}
