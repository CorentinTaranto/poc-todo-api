using Core.Constants;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Results;

namespace Api.Controllers;

[Route("todos")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        this._todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<TodoDto>>>> Get()
    {
        var todos = await this._todoService.GetAllAsync();

        if (todos == null || todos.Count() == 0)
        {
            return this.NotFound(new Response<IEnumerable<TodoDto>>(new ErrorResult
            {
                Code = ErrorCode.ITEMS_NOT_FOUND,
                Description = ErrorDescription.ITEMS_NOT_FOUND
            }));
        }

        return this.Ok(new Response<IEnumerable<TodoDto>>(todos));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<TodoDto>>> Get(string id)
    {
        var todo = await this._todoService.GetByIdAsync(id);

        if (todo == null)
        {
            return this.NotFound(new Response<TodoDto>(new ErrorResult
            {
                Code = ErrorCode.ITEM_NOT_FOUND,
                Description = ErrorDescription.ITEM_NOT_FOUND
            }));
        }

        return this.Ok(new Response<TodoDto>(todo));
    }

    [HttpPost]
    public async Task<ActionResult<Response<TodoDto>>> Post([FromBody] AddTodoDto newTodo)
    {
        if (newTodo == null)
        {
            return this.ValidationProblem();
        }

        var todoCreated = await this._todoService.CreateAsync(newTodo);

        if (todoCreated == null)
        {
            return this.UnprocessableEntity();
        }

        return this.Ok(new Response<TodoDto>(todoCreated));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Response<TodoDto>>> Put(string id, [FromBody] UpdateTodoDto newTodo)
    {
        if (newTodo == null)
        {
            return this.ValidationProblem();
        }

        var todo = await this._todoService.GetByIdAsync(id);

        if (todo == null)
        {
            return this.NotFound(new Response<TodoDto>(new ErrorResult
            {
                Code = ErrorCode.ITEM_NOT_FOUND,
                Description = ErrorDescription.ITEM_NOT_FOUND
            }));
        }

        var updatedTodo = await this._todoService.UpdateAsync(id, newTodo);

        if (updatedTodo == null)
        {
            return this.UnprocessableEntity();
        }

        return this.Ok(new Response<TodoDto>(updatedTodo));
    }

    [HttpPut]
    [Route("section/{id}")]
    public async Task<ActionResult<Response<TodoDto>>> Put(string id)
    {
        var todo = await this._todoService.UpdateToNextSectionAsync(id);

        if (todo == null)
        {
            return this.NotFound(new Response<TodoDto>(new ErrorResult
            {
                Code = ErrorCode.ITEM_NOT_FOUND,
                Description = ErrorDescription.ITEM_NOT_FOUND
            }));
        }

        return this.Ok(new Response<TodoDto>(todo));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var todo = await this._todoService.GetByIdAsync(id);

        if (todo == null)
        {
            return this.NotFound(new Response<bool>(new ErrorResult
            {
                Code = ErrorCode.ITEM_NOT_FOUND,
                Description = ErrorDescription.ITEM_NOT_FOUND
            }));
        }

        var isDeleted = await this._todoService.DeleteAsync(id);

        return isDeleted ? this.Ok(isDeleted) : this.UnprocessableEntity() as IActionResult;
    }
}

