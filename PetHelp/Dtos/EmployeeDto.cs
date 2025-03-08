using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Employee")]
    public class EmployeeDto : BaseDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IdentityBaseDto User { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionHeaderDto> Adoptions { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<ScheduleDto> Schedules { get; set; }
    }
}
