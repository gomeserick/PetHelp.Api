using System.ComponentModel.DataAnnotations;

namespace PetHelp.Dtos.Base
{
    public class BaseDto
    {
        [Key]
        public int Id { get; set; }
    }
}
