namespace PetHelp.Services.Notificator
{
    public interface INotificatorService
    {
        bool HasNotifications();
        void Notify(string attribute, params string[] message);
        Dictionary<string, string[]> GetNotification();
    }
}