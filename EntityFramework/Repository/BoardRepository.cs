using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Database;

namespace EntityFramework.Repository;

public class BoardRepository : BaseRepository<Board>, IBoardRepository
{
    public BoardRepository(TodoContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Board>> GetAllAsync()
    {
        return await this._context.Boards.Include(x => x.Todos).ToListAsync();
    }

    public override async Task<Board> GetByIdAsync(string id)
    {
        return await this._dbSet.Include(x => x.Todos)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }
}

