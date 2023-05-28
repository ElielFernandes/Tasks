using Flunt.Notifications;

namespace Tasks.ViewModels;

public class UpdateTodoViewModel : Notifiable<Notification>
{
    public string Title { get; set; }
}