using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Vaccine")]
    public class VaccineDto: PrivateDataDto
    {
        public string Type { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime NextDate { get; set; }
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
