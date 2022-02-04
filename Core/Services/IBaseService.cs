using Core.Repository;

namespace Core.Services;

public interface IBaseService<DtoType, AddDtoType, ModelType, TRepository>
    where DtoType : class
    where AddDtoType : class
    where ModelType : class
    where TRepository : IBaseRepository<ModelType>
{
    Task<IEnumerable<DtoType>> GetAllAsync();

    Task<DtoType> CreateAsync(AddDtoType obj);

    Task<DtoType> UpdateAsync(string id, DtoType obj);

    Task<DtoType> GetByIdAsync(string id);

    Task<bool> DeleteAsync(string id);
}

