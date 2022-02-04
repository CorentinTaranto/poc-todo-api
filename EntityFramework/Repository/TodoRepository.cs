using Core.Repository;
using Model.Database;

namespace EntityFramework.Repository;

public class TodoRepository : BaseRepository<Todo>, ITodoRepository
{
    public TodoRepository(TodoContext context) : base(context)
    {
    }
}

