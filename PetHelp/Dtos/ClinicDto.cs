using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Clinic")]
    public class ClinicDto : BaseDto
    {
        [MaxLength(40)]
        public string Name { get; set; }
        [StringLength(14)]
        public string Cnpj { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [MaxLength(40)]
        public string License { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public AddressDto Address { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AnimalDto> Animals { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<MedicationDto> Medications { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<VaccineDto> Vaccines { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentHeaderDto> Apointments { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ApointmentResultDto> ApointmentsResults { get; set; }
    }
}
