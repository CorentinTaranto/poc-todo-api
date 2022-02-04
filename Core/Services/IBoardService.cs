using Core.Repository;
using Model.Database;
using Model.DTO;

namespace Core.Services;

public interface IBoardService : IBaseService<BoardDto, AddBoardDto, Board, IBoardRepository>
{

    Task<BoardDto> UpdateAsync(string id, UpdateBoardDto boardDto);

}

