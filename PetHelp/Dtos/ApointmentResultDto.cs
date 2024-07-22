using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("ApointmentResult")]
    public class ApointmentResultDto: PrivateDataDto
    {
        [MaxLength(75)]
        public string Type { get; set; }
        public string Observations { get; set; }
        public DateTime ServiceDate { get; set; }
        [ForeignKey(nameof(ApointmentHeader))]
        public int ApointmentHeaderId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ApointmentHeaderDto ApointmentHeader { get; set; }
    }
}
