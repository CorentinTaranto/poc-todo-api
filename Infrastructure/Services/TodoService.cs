using AutoMapper;
using Core.Repository;
using Core.Services;
using Model.Database;
using Model.DTO;
using Model.Enums;

namespace Infrastructure.Services;

public class TodoService : BaseService<TodoDto, AddTodoDto, Todo, ITodoRepository>, ITodoService
{
    protected readonly IBoardRepository _boardRepository;

    public TodoService(ITodoRepository repository, IMapper mapper, IBoardRepository boardRepository) : base(repository, mapper)
    {
        this._boardRepository = boardRepository;
    }

    public override async Task<TodoDto> CreateAsync(AddTodoDto newTodo)
    {
        if (newTodo == null)
        {
            return null;
        }

        var todo = this._mapper.Map<Todo>(newTodo);

        var isBoardUnlocked = await this._boardRepository.Exists(x => x.Id == todo.BoardId && x.IsLocked == false);

        if (!(await this.CheckTodo(todo)) || !isBoardUnlocked)
        {
            return null;
        }


        var todoCreated = await this._repository.CreateAsync(todo);

        return this._mapper.Map<TodoDto>(todoCreated);
    }

    public async Task<TodoDto> UpdateAsync(string id, UpdateTodoDto todoDto)
    {
        if (todoDto == null)
        {
            return null;
        }

        var todoFromDatabase = await this._repository.GetByIdAsync(id);

        this._mapper.Map<UpdateTodoDto, Todo>(todoDto, todoFromDatabase);

        if (!(await this.CheckTodo(todoFromDatabase)))
        {
            return null;
        }

        var todoUpdated = await this._repository.UpdateAsync(todoFromDatabase);

        return this._mapper.Map<TodoDto>(todoUpdated);
    }

    public async Task<TodoDto> UpdateToNextSectionAsync(string id)
    {
        var todoFromDatabase = await this._repository.GetByIdAsync(id);

        if (todoFromDatabase == null)
        {
            return null;
        }

        switch (todoFromDatabase.Section)
        {
            case Section.Todo:
                todoFromDatabase.Section = Section.Doing;
                break;
            case Section.Doing:
                todoFromDatabase.Section = Section.Done;
                break;
        }

        var todoUpdated = await this._repository.UpdateAsync(todoFromDatabase);

        return this._mapper.Map<TodoDto>(todoUpdated);
    }

    private async Task<bool> CheckTodo(Todo todo)
    {
        var isBoardExisting = await this._boardRepository.Exists(x => x.Id == todo.BoardId);

        if (!isBoardExisting || !Enum.IsDefined(typeof(Section), todo.Section))
        {
            return false;
        }

        return true;
    }
}

