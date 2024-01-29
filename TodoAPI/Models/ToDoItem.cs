namespace TodoAPI.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public bool IsCompleted { get; set; }

        private ToDoItem(Guid id, string title, string description, int priority, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            IsCompleted = isCompleted;
        }

        public static ToDoItem Create(string title, string description, int priority)
        {
            return new ToDoItem(Guid.NewGuid(), title, description, priority, false);
        }
    }
}
