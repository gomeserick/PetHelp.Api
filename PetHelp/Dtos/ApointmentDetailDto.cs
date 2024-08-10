using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace PetHelp.Dtos
{
    public class ApointmentDetailDto: PrivateDataDto
    {
        public int ApointmentHeaderId { get; set; }
        public int AnimalId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AnimalDto Animal { get; set; }
    }
}
