using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Apointment")]
    public class ApointmentHeaderDto: PrivateDataDto
    {
        [MaxLength(75)]
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        [ForeignKey(nameof(Clinic))]
        public int ClinicId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ClinicDto Clinic { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentDetailDto> Details { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentResultDto> Results { get; set; }
    }
}
