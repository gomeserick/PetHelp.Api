using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Schedule")]
    public class ScheduleDto: PrivateDataDto
    {
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AnimalDto Animal { get; set; }
    }
}
