namespace Tasks.Models;
public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
    
    public Todo(Guid Id, string Title, bool Done) {
        this.Id = Id;
        this.Title = Title;
        this.Done = Done;
    }
}