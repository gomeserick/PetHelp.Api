using PetHelp.Dtos.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos
{
    [Table("AdoptionDetail")]
    public class AdoptionDetailDto: PrivateDataDto
    {
        [ForeignKey(nameof(AdoptionHeader))]
        public int AdoptionHeaderId { get; set; }
        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }
        public string Observation { get; set; }
        public AdoptionHeaderDto AdoptionHeader { get; set; }
        public AnimalDto Animal { get; set; }
    }
}
