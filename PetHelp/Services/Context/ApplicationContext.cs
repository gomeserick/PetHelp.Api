using PetHelp.Services.Context.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PetHelp.Services.Context
{
    public class ApplicationContext
    {
        public ApplicationFiles Files { get; set; }
    }

    public class ApplicationFiles
    {
        public string ImagePath { get; set; }
    }
}
