using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Animal")]
    public class AnimalDto : BaseDto
    {
        [MaxLength(75)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Species { get; set; }
        [MaxLength(50)]
        public string Breed { get; set; }
        [MaxLength(20)]
        public string Color { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        public string Temperament { get; set; }
        public string Image { get; set; }
        public bool Available { get; set; }
        public bool Alive { get; set; }
        public bool Castrated { get; set; }
        [ForeignKey(nameof(Clinic))]
        public int ClinicId { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public ClinicDto Clinic { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public UserDto User { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<WatchedDto> WatchedList { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentResultDto> ApointmentResults { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentDetailDto> Apointments { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<VaccineDto> Vaccines { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<MedicationDto> Medications { get; set; }
        
    }
}
