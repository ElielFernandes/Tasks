using Flunt.Notifications;
using Flunt.Validations;
using Tasks.Models;

namespace Tasks.ViewModels;

public class CreateTodoViewModel : Notifiable<Notification>
{
    public string? Title { get; set; }

    public Todo MapTo() 
    {
       AddNotifications(new Contract<Notification>()
           .Requires()
           .IsNotNull(Title, "Informe o título da tarefa")
           .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracteres")
       );

       return new Todo(Guid.NewGuid(), Title, false);
    }
}