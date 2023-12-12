namespace PetHelp.Services.Notificator
{
    public class NotificatorService : INotificatorService
    {
        private Dictionary<string, string[]> _notifications;
        public NotificatorService()
        {
            _notifications = new Dictionary<string, string[]>();
        }

        public void Notify(string attribute, params string[] message)
        {
            _notifications.Add(attribute, message);
        }

        public bool HasNotifications()
        {
            return _notifications.Count > 0;
        }

        public Dictionary<string, string[]> GetNotification()
        {
            return _notifications;
        }
    }
}
