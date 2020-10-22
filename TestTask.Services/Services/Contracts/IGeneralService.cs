using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.DataBase;

namespace TestTask.Services.Services.Contracts
{
    public interface IGeneralService<T, TDto>
    {
        /// <summary>
        /// Добавить запись в базу
        /// </summary>
        /// <param name="basketCreateDto">Объект добавление</param>
        /// <returns></returns>
        Task<EntityOperationResult<T>> CreateAsync(TDto basketCreateDto);

        Task<EntityOperationResult<T>> UpdateAsync(TDto offerDto);

        

        /// <summary>
        /// Вернуть все записи
        /// </summary>
        /// <returns></returns>
        Task<List<TDto>> GetAllAsync();
        Task<List<TDto>> GetPageAsync(int numberPage, int size);
    }

    public interface IGeneralService<T, TDto, TId> : IGeneralService<T, TDto>
        where T : IEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Вернуть конкретный объект из базы
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns></returns>
        Task<TDto> GetByIdAsync(TId id);

        Task<EntityOperationResult<T>> DeleteItemAsync(TId id);
    }
}
