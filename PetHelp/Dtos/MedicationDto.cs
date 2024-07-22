using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Medication")]
    public class MedicationDto: PrivateDataDto
    {
        [MaxLength(75)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        [MaxLength(50)]
        public int Dose { get; set; }
        [MaxLength(50)]
        public string DoseUnitOfMeasurement { get; set; }
        public TimeSpan? Frequency { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }
        [ForeignKey(nameof(Clinic))]
        public int ClinicId { get; set; }
        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public ClinicDto Clinic { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AnimalDto Animal { get; set; }
    }
}
