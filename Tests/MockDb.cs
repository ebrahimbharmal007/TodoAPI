using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI;

namespace Tests
{
    public class MockDb : IDbContextFactory<TodoDb>
    {
        public TodoDb CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoDb>()
                .UseInMemoryDatabase($"InMemoryTestDb")
                .Options;

            return new TodoDb(options);
        }
    }
}
