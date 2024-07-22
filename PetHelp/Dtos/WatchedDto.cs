using PetHelp.Dtos.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("Watched")]
    public class WatchedDto: PrivateDataDto
    {
        public bool Favorite { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public AnimalDto Animal { get; set; }
    }
}
