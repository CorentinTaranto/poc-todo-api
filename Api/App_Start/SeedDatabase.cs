using Core.Services;
using EntityFramework;
using Model.DTO;

namespace Api.App_Start;

public class SeedDatabase
{
    public static async Task Initialize(TodoContext context,
        IBoardService boardService,
        ITodoService todoService)
    {
        context.Database.EnsureCreated();


        // Creation of the boards.
        var board1 = await boardService.CreateAsync(new AddBoardDto
        {
            Title = "Board 1"
        });

        var board2 = await boardService.CreateAsync(new AddBoardDto
        {
            Title = "Board 2"
        });

        //Creation of the todos.
        await todoService.CreateAsync(new AddTodoDto
        {
            Title = "Todo 1",
            Deadline = DateTime.Now,
            Description = "Description 1",
            BoardId = board1.Id
        });

        await todoService.CreateAsync(new AddTodoDto
        {
            Title = "Todo 2",
            Deadline = DateTime.Now,
            Description = "Description 2",
            BoardId = board1.Id
        });

        await todoService.CreateAsync(new AddTodoDto
        {
            Title = "Todo 3",
            Deadline = DateTime.Now,
            Description = "Description 3",
            BoardId = board1.Id
        });

        await todoService.CreateAsync(new AddTodoDto
        {
            Title = "Todo 4",
            Deadline = DateTime.Now,
            Description = "Description 4",
            BoardId = board2.Id
        });
    }
}

