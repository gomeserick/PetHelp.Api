using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PetHelp.Services.Notificator
{
    public interface INotificatorService
    {
        ModelStateDictionary GetNotifications();
        bool HasNotifications();
        void Notify(string key, string message);
        void Notify(ModelStateDictionary modelState);
    }
}