using Microsoft.AspNetCore.Http.HttpResults;
using TodoAPI;
using TodoAPI.Models;

namespace Tests
{
    public class ToDoApiTests
    {
        [Fact]
        public async Task CreateTodo_Returns_Valid_Object()
        {
            //Arrange
            await using var context = new MockDb().CreateDbContext();

            ToDoItemDTO newTodo = new ToDoItemDTO("title", "desc", 0);

            //Act
            var result = await TodoEndpoints.CreateTodo(newTodo, context);

            //Assert
            Assert.IsType<Created<ToDoItem>>(result);

            Assert.NotNull(result);
            
        }
    }
}