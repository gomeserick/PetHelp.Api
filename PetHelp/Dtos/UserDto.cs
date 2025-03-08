using Microsoft.AspNetCore.Identity;
using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("User")]
    public class UserDto : BaseDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }
        [ForeignKey(nameof(IdentityUser))]
        public int UserId { get; set; }
        public IdentityBaseDto IdentityUser { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionHeaderDto> Adoptions { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionDetailDto> AdoptionsDetails { get; set;}
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AnimalDto> Animals { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentHeaderDto> Apointments { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentResultDto> Results { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<MedicationDto> Medications { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<VaccineDto> Vaccines { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<WatchedDto> WatchedList { get; set; }
    }
}
