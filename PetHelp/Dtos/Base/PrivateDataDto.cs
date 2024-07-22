using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelp.Dtos.Base
{
    public abstract class PrivateDataDto: BaseDto
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        [SwaggerSchema(ReadOnly = true)]
        public virtual UserDto User { get; set; }
    }
}
