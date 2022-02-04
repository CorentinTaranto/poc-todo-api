using Model.Database;
using Model.DTO;

namespace Api.App_Start;

public class MapperProfile : AutoMapper.Profile
{
    public MapperProfile()
    {
        this.CreateMap<AddBoardDto, Board>();

        this.CreateMap<UpdateBoardDto, Board>();

        this.CreateMap<Board, BoardDto>();

        this.CreateMap<AddTodoDto, Todo>();

        this.CreateMap<UpdateTodoDto, Todo>();

        this.CreateMap<Todo, TodoDto>();
    }
}

