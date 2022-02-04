using AutoMapper;
using Core.Repository;
using Core.Services;

namespace Infrastructure.Services;

public class BaseService<DtoType, AddDtoType, ModelType, TRepository> : IBaseService<DtoType, AddDtoType, ModelType, TRepository>
    where DtoType : class
    where AddDtoType : class
    where ModelType : class
    where TRepository : IBaseRepository<ModelType>
{
    protected TRepository _repository;
    protected readonly IMapper _mapper;

    public BaseService(TRepository repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public virtual async Task<DtoType> CreateAsync(AddDtoType obj)
    {
        if (obj == null)
        {
            return null;
        }

        var addObject = this._mapper.Map<ModelType>(obj);

        var incomeCreated = await this._repository.CreateAsync(addObject);

        return this._mapper.Map<DtoType>(incomeCreated);
    }

    public virtual async Task<IEnumerable<DtoType>> GetAllAsync()
    {
        var objects = await this._repository.GetAllAsync();

        return this._mapper.Map<IEnumerable<DtoType>>(objects);
    }

    public virtual async Task<DtoType> GetByIdAsync(string id)
    {
        var obj = await this._repository.GetByIdAsync(id);

        return this._mapper.Map<DtoType>(obj);
    }

    public virtual async Task<DtoType> UpdateAsync(string id, DtoType obj)
    {
        if (obj == null)
        {
            return null;
        }

        var objectFromDatabase = await this._repository.GetByIdAsync(id);

        this._mapper.Map(obj, objectFromDatabase);

        var updateObject = await this._repository.UpdateAsync(objectFromDatabase);

        return this._mapper.Map<DtoType>(obj);
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        return await this._repository.DeleteAsync(id);
    }
}

