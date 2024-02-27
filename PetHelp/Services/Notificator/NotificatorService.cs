using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PetHelp.Services.Notificator
{
    public class NotificatorService : INotificatorService
    {
        private readonly Dictionary<string, string> _notifications;

        public NotificatorService()
        {
            _notifications = new Dictionary<string, string>();
        }

        // Method to add a notification with a specific key and message
        public void Notify(string key, string message)
        {
            _notifications[key] = message;
        }

        // Method to add notifications from a ModelStateDictionary
        public void Notify(ModelStateDictionary modelState)
        {
            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Notify(entry.Key, error.ErrorMessage);
                }
            }
        }

        // Method to check if there are any notifications
        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        // Method to get the notifications as a ModelStateDictionary
        public ModelStateDictionary GetNotifications()
        {
            var modelState = new ModelStateDictionary();
            foreach (var notification in _notifications)
            {
                modelState.AddModelError(notification.Key, notification.Value);
            }
            return modelState;
        }
    }
}
