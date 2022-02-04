using Core.Repository;
using Model.Database;
using Model.DTO;

namespace Core.Services;

public interface ITodoService : IBaseService<TodoDto, AddTodoDto, Todo, ITodoRepository>
{
    Task<TodoDto> UpdateAsync(string id, UpdateTodoDto todoDto);

    Task<TodoDto> UpdateToNextSectionAsync(string id);
}

