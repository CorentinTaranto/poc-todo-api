using System.Linq.Expressions;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Database;

namespace EntityFramework.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly TodoContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(TodoContext context)
    {
        this._context = context;
        this._dbSet = this._context.Set<TEntity>();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity obj)
    {
        if (await this._dbSet.AnyAsync(x => x.Id == obj.Id))
        {
            return null;
        }

        var result = await this._dbSet.AddAsync(obj);

        var changes = await this._context.SaveChangesAsync();

        return changes > 0 ? result.Entity : null;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await this._dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(string id)
    {
        return await this._dbSet.FindAsync(id);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity obj)
    {
        if (obj == null)
        {
            return null;
        }

        this._context.Update(obj);

        var changes = await this._context.SaveChangesAsync();

        return changes > 0 ? obj : null;
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        var objToDelete = await this._dbSet.FindAsync(id);

        if (objToDelete == null)
        {
            return false;
        }

        this._dbSet.Remove(objToDelete);

        await this._context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return await this._dbSet.AnyAsync(predicate);
    }
}

