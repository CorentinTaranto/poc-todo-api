using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Model.DTO;
using Model.Results;
using Core.Constants;

namespace Api.Controllers;

[Route("boards")]
[ApiController]
public class BoardsController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardsController(IBoardService boardService)
    {
        this._boardService = boardService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<BoardDto>>>> Get()
    {
        var boards = await this._boardService.GetAllAsync();

        if (boards == null || boards.Count() == 0)
        {
            return this.NotFound(new Response<IEnumerable<BoardDto>>(new ErrorResult
            {
                Code = ErrorCode.ITEMS_NOT_FOUND,
                Description = ErrorDescription.ITEMS_NOT_FOUND
            }));
        }

        return this.Ok(new Response<IEnumerable<BoardDto>>(boards));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Response<BoardDto>>> Get(string id)
    {
        var board = await this._boardService.GetByIdAsync(id);

        if (board == null)
        {
            return this.NotFound(new Response<BoardDto>(new ErrorResult
            {
                Code = ErrorCode.ITEM_NOT_FOUND,
                Description = ErrorDescription.ITEM_NOT_FOUND
            }));
        }

        return this.Ok(new Response<BoardDto>(board));
    }

    [HttpPost]
    public async Task<ActionResult<Response<BoardDto>>> Post([FromBody] AddBoardDto newBoard)
    {
        if (newBoard == null)
        {
            return this.ValidationProblem();
        }

        var createdBoard = await this._boardService.CreateAsync(newBoard);

        if (createdBoard == null)
        {
            return this.UnprocessableEntity();
        }

        return this.Ok(new Response<BoardDto>(createdBoard));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Response<BoardDto>>> Put(string id, [FromBody] UpdateBoardDto updateBoardDto)
    {
        if (updateBoardDto == null)
        {
            return this.ValidationProblem();
        }

        var board = await this._boardService.GetByIdAsync(id);

        if (board == null)
        {
            return this.NotFound(new Response<BoardDto>(new ErrorResult
            {
                Code = ErrorCode.ITEM_NOT_FOUND,
                Description = ErrorDescription.ITEM_NOT_FOUND
            }));
        }

        var updatedBoard = await this._boardService.UpdateAsync(id, updateBoardDto);

        if (updatedBoard == null)
        {
            return this.UnprocessableEntity();
        }

        return this.Ok(new Response<BoardDto>(updatedBoard));
    }
}

