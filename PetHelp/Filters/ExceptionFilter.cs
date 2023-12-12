using Microsoft.AspNetCore.Mvc.Filters;

namespace PetHelp.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}
