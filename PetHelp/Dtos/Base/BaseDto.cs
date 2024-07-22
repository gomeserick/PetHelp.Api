using System.ComponentModel.DataAnnotations;

namespace PetHelp.Dtos.Base
{
    public abstract class BaseDto
    {
        [Key]
        public int Id { get; set; }
    }
}
