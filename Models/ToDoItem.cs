namespace ToDoList.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
