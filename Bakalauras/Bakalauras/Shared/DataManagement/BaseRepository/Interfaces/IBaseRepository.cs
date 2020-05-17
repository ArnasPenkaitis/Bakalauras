using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Shared.DataManagement.BaseRepository.Interfaces
{
    public interface IBaseRepository
    {
        Task<T> CreateAsync<T>(T data) where T : class, IBaseEntity;
        Task<T> QueryByIdAsync<T>(int id) where T : class, IBaseEntity;
        Task<IQueryable<T>> QueryAsync<T>() where T : class, IBaseEntity;
        Task UpdateAsync<T>(int id, T data) where T : class, IBaseEntity;
        Task DeleteAsync<T>(int id) where T : class, IBaseEntity;
        Task DeleteBulk<T>(IQueryable<int> ids) where T : class, IBaseEntity;
        Task SaveChangesAsync();
    }
}
