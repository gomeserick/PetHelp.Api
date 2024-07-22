using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("AdoptionHeader")]
    public class AdoptionHeaderDto : PrivateDataDto
    {
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public EmployeeDto Employee { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<AdoptionDetailDto> AdoptionDetails { get; set; }
    }
}
