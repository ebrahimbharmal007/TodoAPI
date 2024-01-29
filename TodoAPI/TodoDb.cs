namespace TodoAPI
{
    using Microsoft.EntityFrameworkCore;
    using TodoAPI.Models;

    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<ToDoItem> Todos => Set<ToDoItem>();
    }
}
