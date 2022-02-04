using AutoMapper;
using Core.Repository;
using Core.Services;
using Model.Database;
using Model.DTO;

namespace Infrastructure.Services;

public class BoardService : BaseService<BoardDto, AddBoardDto, Board, IBoardRepository>, IBoardService
{
    public BoardService(IBoardRepository repository, IMapper mapper) : base(repository, mapper)
    {

    }

    public async Task<BoardDto> UpdateAsync(string id, UpdateBoardDto boardDto)
    {
        var boardFromDatabase = await this._repository.GetByIdAsync(id);

        if (boardFromDatabase == null)
        {
            return null;
        }

        this._mapper.Map(boardDto, boardFromDatabase);

        var updatedBoard = await this._repository.UpdateAsync(boardFromDatabase);

        return this._mapper.Map<BoardDto>(updatedBoard);
    }

    public override async Task<BoardDto> CreateAsync(AddBoardDto newBoard)
    {
        if (newBoard == null)
        {
            return null;
        }

        var addBoard = this._mapper.Map<Board>(newBoard);

        addBoard.IsLocked = false;

        var boardCreated = await this._repository.CreateAsync(addBoard);

        return this._mapper.Map<BoardDto>(boardCreated);
    }
}
