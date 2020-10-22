using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Core.Contracts;
using TestTask.Core.DataBase;
using TestTask.Services.Dto;
using TestTask.Services.Services.Contracts;

namespace TestTask.Services.Services
{
    public abstract class GeneralService<T, TDto> : IGeneralService<T, TDto>
        where T : class, IEntity
        where TDto : class, IServiceDto

    {
        public GeneralService(TDto dtoEmpty,
                              IMapper mapper,
                              IRepository<T> repository)
        {
            if (dtoEmpty == null)
                throw new ArgumentNullException(nameof(dtoEmpty));

            this.dtoEmpty = dtoEmpty;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            repositoryBase = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected readonly TDto dtoEmpty;
        protected readonly IMapper mapper;
        protected readonly IRepository<T> repositoryBase;
        //this mock
        protected abstract string CheckBeforeModification(TDto value, bool isNew = true);

        public virtual async Task<EntityOperationResult<T>> CreateAsync(TDto Dto)
        {
            string errors = CheckBeforeModification(Dto);
            if (!string.IsNullOrEmpty(errors))
            {
                return EntityOperationResult<T>.Failure().AddError(errors);
            }

            try
            {
                T value = mapper.Map<T>(Dto);
                var entity = await repositoryBase.CreateAsync(value);

                return EntityOperationResult<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<T>.Failure().AddError(ex.Message);
            }
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var list = await repositoryBase.GetAllAsync();

            if (list == null || list.Count == 0)
            {
                return new List<TDto>();
            }
            return mapper.Map<List<TDto>>(list);
        }

        public async Task<List<TDto>> GetPageAsync(int numberPage, int size)
        {
            var page = await repositoryBase.GetPageAsync(numberPage, size);

            if (page == null || page.Count == 0)
            {
                return new List<TDto>();
            }
            var list = mapper.Map<List<TDto>>(page);
            return list;
        }

        public async Task<EntityOperationResult<T>> UpdateAsync(TDto dto)
        {
            try
            {
                T value = mapper.Map<T>(dto);
                repositoryBase.Update(value);
                
                return EntityOperationResult<T>.Success(value);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<T>.Failure().AddError(ex.Message);
            }
        }
    }

    public abstract class GeneralService<T, TDto, TId> : GeneralService<T, TDto>, IGeneralService<T, TDto, TId>
        where T : class, IEntity<TId>
        where TDto : class, IServiceDto<TId>
        where TId : IEquatable<TId>
    {
        protected readonly IRepository<T, TId> extendedRepositoryBase;

        public GeneralService(TDto empty,
                              IMapper mapper,
                              IRepository<T, TId> repository)
                              : base(empty, mapper, repository)
        {
            extendedRepositoryBase = repositoryBase as IRepository<T, TId>;
        }

        protected abstract string CkeckBeforeDelete(T value);

        public async Task<EntityOperationResult<T>> DeleteItemAsync(TId id)
        {
            try
            {
                var entity = await extendedRepositoryBase.GetByIdAsync(id);
                if (entity  == null)
                {
                    return EntityOperationResult<T>.Failure().AddError("Не найдена запись");
                }
                

                string error = CkeckBeforeDelete(entity);
                if (!string.IsNullOrEmpty(error))
                {
                    return EntityOperationResult<T>.Failure().AddError(error);
                }

                extendedRepositoryBase.Delete(entity);

                return EntityOperationResult<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return EntityOperationResult<T>.Failure().AddError(ex.Message);
            }
        }
        
        public async Task<TDto> GetByIdAsync(TId id)
        {
            T value = await extendedRepositoryBase.GetByIdAsync(id);
            if (value == null) return dtoEmpty;
            TDto dto = mapper.Map<TDto>(value);
            return dto;
        }
    }
}
